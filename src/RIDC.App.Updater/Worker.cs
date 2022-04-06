using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RIDC.Database;
using RIDC.Provider.Configuration;
using RIDC.Provider.Configuration.Options;
using RIDC.Provider.Database;
using RIDC.Schema;
using RIDC.Schema.Json;
using RIDC.Schema.Json.Mapper;
using RIDC.Storage.Base;

// ReSharper disable StringLiteralTypo

namespace RIDC.App.Updater;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    private readonly DirectoryInfo _repoDirectory;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;

        _repoDirectory = new DirectoryInfo(Path.Combine(configuration["DataDirectory"], "repo"));

        ReCreateRepoDirectory();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested is false)
        {
            await Run(stoppingToken);
            await Task.Delay(RidcConfigurationProvider.GetProvider().GetOption<UpdaterOption>().Interval * 60 * 1000, stoppingToken);
        }
    }

    private async Task Run(CancellationToken stoppingToken)
    {
        _logger.LogInformation("更新检查开始运行：{UpdateStartTime}", DateTimeOffset.Now);
        var tmpFolder = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));
        tmpFolder.Create();

        try
        {
            // 本次更新进程 Scope
            using var scope = _serviceProvider.CreateScope();

            // 更新配置项
            var option = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<UpdaterOption>>();
            if (OptionValidation(option) is false)
            {
                throw new Exception("更新配置错误");
            }

            // 数据库和 HttpClient
            await using var db = scope.ServiceProvider.GetRequiredService<RhodesIslandDbContext>();
            var requireUpdate = db.IsRequireUpdate();
            var proxy = string.IsNullOrEmpty(option.Value.Proxy) ? null : new WebProxy(option.Value.Proxy);
            using var client = new HttpClient(new HttpClientHandler
            {
                Proxy = proxy, UseProxy = proxy is not null, AllowAutoRedirect = true
            });
            client.DefaultRequestHeaders.Add("User-Agent", option.Value.UserAgent);
            client.Timeout = TimeSpan.FromMinutes(10);

            // 存储服务
            IRidcStorage? storage = null;
            if (option.Value.HaveStorage)
            {
                storage = scope.ServiceProvider.GetRequiredService<IRidcStorage>();
            }

            // 计时器
            var sw = new Stopwatch();
            sw.Start();

            #region Request GitHub

            var versionResponse = await client.GetAsync(
                "https://raw.githubusercontent.com/yuanyan3060/Arknights-Bot-Resource/main/version", stoppingToken);
            if (versionResponse.IsSuccessStatusCode is false)
            {
                _logger.LogError("发送请求至 {Url} 失败",
                    "https://raw.githubusercontent.com/yuanyan3060/Arknights-Bot-Resource/main/version");
                return;
            }

            var versionString = await versionResponse.Content.ReadAsStringAsync(stoppingToken);
            _logger.LogDebug("从 GitHub 获取到版本：{GitHubVersion}", versionString);

            #endregion

            #region Version Check

            var needUpdate = requireUpdate;
            if (requireUpdate is false)
            {
                var inStoreVersion = db.Miscellaneous.AsNoTracking().FirstOrDefault(x => x.Key == "version");
                if (inStoreVersion?.Value == versionString)
                {
                    _logger.LogInformation("数据库版本无变化，{DatabaseVersion}", inStoreVersion.Value);
                }
                else
                {
                    _logger.LogInformation("数据库版本更新：{DatabaseVersion} -> {GitHubVersion}", inStoreVersion?.Value,
                        versionString);
                    needUpdate = true;
                }
            }
            else
            {
                _logger.LogInformation("数据库架构不存在，数据库版本更新 null -> {GitHubVersion}", versionString);
            }

            if (storage is not null)
            {
                var storageVersion = await storage.GetBlobVersionAsync();
                if (storageVersion == versionString)
                {
                    _logger.LogInformation("存储版本无变化，{StorageVersion}", storageVersion);
                }
                else
                {
                    _logger.LogInformation("存储版本更新：{StorageVersion} -> {GitHubVersion}", storageVersion, versionString);
                    needUpdate = true;
                }
            }

            if (needUpdate is false)
            {
                return;
            }

            _logger.LogInformation("开始更新进程，{UpdateStartTime}", DateTimeOffset.Now);

            #endregion

            #region Get Repo

            sw.Stop();
            var downloadStopwatch = new Stopwatch();
            downloadStopwatch.Start();
            switch (option.Value.Method)
            {
                case "Download":
                    {
                        _logger.LogDebug("使用下载方式获取更新");

                        #region Download Repo

                        var downloadRepoResponse =
                            await client.GetAsync(
                                "https://api.github.com/repos/yuanyan3060/Arknights-Bot-Resource/zipball",
                                stoppingToken);
                        if (downloadRepoResponse.IsSuccessStatusCode is false)
                        {
                            _logger.LogError("Repo 下载失败");
                            return;
                        }

                        var tmpFile = Path.Combine(tmpFolder.FullName, Guid.NewGuid().ToString());
                        await using var fs = File.Create(tmpFile);
                        var downloadFileStream = await downloadRepoResponse.Content.ReadAsStreamAsync(stoppingToken);
                        await downloadFileStream.CopyToAsync(fs, stoppingToken);
                        fs.Close();

                        ZipFile.ExtractToDirectory(tmpFile, tmpFolder.FullName);
                        var downloadRepoDir = tmpFolder.GetDirectories().First();
                        _repoDirectory.Delete();
                        downloadRepoDir.MoveTo(_repoDirectory.FullName);
                        _logger.LogDebug("下载文件完成");

                        #endregion

                        break;
                    }
                case "Clone":
                    {
                        #region Clone Or Pull Repo

                        // TODO: 使用 Git 进行更新，LibGit2Sharp 存在问题无法使用，寻找替换方案
                        throw new NotImplementedException("Git Clone / Git Pull 未实现");

                        #endregion
                    }
            }
            downloadStopwatch.Stop();
            _logger.LogInformation("获取 Repo 耗时：{DownloadRepoTime} ms", GetMilliSeconds(downloadStopwatch.ElapsedTicks));
            sw.Start();

            #endregion

            #region Read Files

            var jsonCharacterStr =
                await File.ReadAllTextAsync(
                    Path.Combine(_repoDirectory.FullName, "gamedata/excel/character_table.json"), stoppingToken);
            var jsonCharacterPatchStr =
                await File.ReadAllTextAsync(
                    Path.Combine(_repoDirectory.FullName, "gamedata/excel/char_patch_table.json"), stoppingToken);
            var jsonCharmStr =
                await File.ReadAllTextAsync(
                    Path.Combine(_repoDirectory.FullName, "gamedata/excel/charm_table.json"), stoppingToken);
            var jsonItemStr =
                await File.ReadAllTextAsync(
                    Path.Combine(_repoDirectory.FullName, "gamedata/excel/item_table.json"), stoppingToken);
            var jsonPowerStr =
                await File.ReadAllTextAsync(
                    Path.Combine(_repoDirectory.FullName, "gamedata/excel/handbook_team_table.json"), stoppingToken);
            var jsonSkillStr =
                await File.ReadAllTextAsync(
                    Path.Combine(_repoDirectory.FullName, "gamedata/excel/skill_table.json"), stoppingToken);
            var jsonStageStr =
                await File.ReadAllTextAsync(
                    Path.Combine(_repoDirectory.FullName, "gamedata/excel/stage_table.json"), stoppingToken);
            var jsonTipStr =
                await File.ReadAllTextAsync(
                    Path.Combine(_repoDirectory.FullName, "gamedata/excel/tip_table.json"), stoppingToken);
            var jsonZoneStr =
                await File.ReadAllTextAsync(
                    Path.Combine(_repoDirectory.FullName, "gamedata/excel/zone_table.json"), stoppingToken);
            var jsonEnemyStr =
                await File.ReadAllTextAsync(
                    Path.Combine(_repoDirectory.FullName, "gamedata/excel/enemy_handbook_table.json"), stoppingToken);
            var jsonSkinStr =
                await File.ReadAllTextAsync(
                    Path.Combine(_repoDirectory.FullName, "gamedata/excel/skin_table.json"), stoppingToken);

            #endregion

            #region Deserialize Data

            #region Power

            var powerArray = JsonDocument.Parse(jsonPowerStr)
                .RootElement
                .EnumerateObject();
            var powers = powerArray
                .Select(x => x.Value)
                .Select(x => x.Deserialize<JsonPower>())
                .Select(x => x.ToPower())
                .ToList();

            #endregion

            #region Skill

            var skillArray = JsonDocument.Parse(jsonSkillStr)
                .RootElement
                .EnumerateObject();
            var skills = skillArray
                .Select(x => x.Value)
                .Select(x => x.Deserialize<JsonSkill>())
                .Select(x => x.ToSkill())
                .ToList();

            #endregion

            #region Skin

            var skinArray = JsonDocument.Parse(jsonSkinStr)
                .RootElement
                .GetProperty("charSkins")
                .EnumerateObject();
            var skins = skinArray
                .Select(x => x.Value)
                .Select(x => x.Deserialize<JsonSkin>())
                .Select(x => x.ToSkin())
                .ToList();

            #endregion

            #region Character

            var patchCharacterArray = JsonDocument.Parse(jsonCharacterPatchStr)
                .RootElement
                .GetProperty("patchChars")
                .EnumerateObject()
                .ToList();
            var characterArray = JsonDocument.Parse(jsonCharacterStr)
                .RootElement
                .EnumerateObject()
                .ToList();
            characterArray.AddRange(patchCharacterArray);
            var characters = characterArray
                .Select(x => x.Value
                    .Deserialize<JsonCharacter>()
                    .ToCharacter(x.Name, powers, skills, skins))
                .ToList();

            #endregion

            #region Stage

            var stageArray = JsonDocument.Parse(jsonStageStr)
                .RootElement
                .GetProperty("stages")
                .EnumerateObject();
            var stages = stageArray
                .Select(x => x.Value)
                .Select(x => x.Deserialize<JsonStage>())
                .Select(x => x.ToStage())
                .ToList();

            #endregion

            #region Zone

            var zoneArray = JsonDocument.Parse(jsonZoneStr)
                .RootElement
                .GetProperty("zones")
                .EnumerateObject();
            var zones = zoneArray
                .Select(x => x.Value)
                .Select(x => x.Deserialize<JsonZone>())
                .Select(x => x.ToZone(stages))
                .ToList();

            #endregion

            #region Charm

            var charmArray = JsonDocument.Parse(jsonCharmStr)
                .RootElement
                .GetProperty("charmList")
                .EnumerateArray();
            var charms = charmArray
                .Select(x => x.Deserialize<JsonCharm>())
                .Select(x => x.ToCharm())
                .ToList();

            #endregion

            #region Item

            var itemArray = JsonDocument.Parse(jsonItemStr)
                .RootElement
                .GetProperty("items")
                .EnumerateObject();
            var items = itemArray
                .Select(x => x.Value)
                .Select(x => x.Deserialize<JsonItem>())
                .Select(x => x.ToItem())
                .ToList();

            #endregion

            #region Tip

            var tipArray = JsonDocument.Parse(jsonTipStr)
                .RootElement
                .GetProperty("tips")
                .EnumerateArray();
            var tips = tipArray
                .Select(x => x.Deserialize<JsonTip>())
                .Select(x => x.ToTip())
                .ToList();

            #endregion

            #region Enemy

            var enemyArray = JsonDocument.Parse(jsonEnemyStr)
                .RootElement
                .EnumerateObject();
            var enemies = enemyArray
                .Select(x => x.Value)
                .Select(x => x.Deserialize<JsonEnemy>())
                .Select(x => x.ToEnemy())
                .ToList();

            #endregion

            #endregion Deserialize Data

            GC.Collect();

            #region Rebuild Database

            await db.Database.EnsureDeletedAsync(stoppingToken);
            await db.UpdateDatabase(_logger);

            #endregion

            #region Update Database

            await db.Characters.AddRangeAsync(characters, stoppingToken);
            await db.Charms.AddRangeAsync(charms, stoppingToken);
            await db.Zones.AddRangeAsync(zones, stoppingToken);
            await db.Charms.AddRangeAsync(charms, stoppingToken);
            await db.Items.AddRangeAsync(items, stoppingToken);
            await db.Tips.AddRangeAsync(tips, stoppingToken);
            await db.Enemies.AddRangeAsync(enemies, stoppingToken);

            #endregion

            #region Set Database Version

            var version = new Miscellaneous { Key = "version", Value = versionString };
            await db.Miscellaneous.AddAsync(version, stoppingToken);

            #endregion

            var changes = await db.SaveChangesAsync(stoppingToken);

            // 数据库更新计时器结束
            sw.Stop();

            _logger.LogInformation("数据库更新运行结束，耗时 {DbUpdateTime} ms，{UpdateDbChanges} 条数据库修改记录",
                GetMilliSeconds(sw.ElapsedTicks), changes);

            // 不存在 Storage 则结束
            if (storage is null)
            {
                await db.DisposeAsync();
                scope.Dispose();
                return;
            }

            sw.Restart();

            #region Storage Update

            var fileKeyPaths = new[] { "avatar", "item", "portrait", "skill", "enemy" };

            foreach (var fileKeyPath in fileKeyPaths)
            {
                var localFile = await GetLocalBlobs(fileKeyPath);
                var remoteFile = await storage.GetBlobsAsync(fileKeyPath) ?? new List<BlobFileInfo>();

                remoteFile.Compare(localFile, out var deletedBlobs, out var addedBlobs);

                var deleteBlobStatus = await storage.DeleteBlobsAsync(deletedBlobs.Select(x => x.Key));
                if (deleteBlobStatus is false)
                {
                    // TODO: 需要寻找更好的方法处理这个错误
                    throw new Exception("删除文件失败");
                }

                var addedBlobInfos = addedBlobs
                    .Select(x => new LocalBlobFileInfo(x.Key, x.Hash, Path.Combine(_repoDirectory.FullName, x.Key)));

                var addBlobStatus = await storage.UploadBlobsAsync(addedBlobInfos);
                if (addBlobStatus is false)
                {
                    // TODO: 需要寻找更好的方法处理这个错误
                    throw new Exception("上传文件失败");
                }
            }

            await storage.DeleteBlobsAsync(new[] { "version.txt" });
            var versionFilePath = Path.Combine(_repoDirectory.FullName, "version");
            await using var versionFileStream = File.OpenRead(versionFilePath);
            var versionFileHash = await versionFileStream.GetMd5();
            versionFileStream.Close();
            await storage.UploadBlobsAsync(new[] { new LocalBlobFileInfo("version.txt", versionFileHash, versionFilePath) });

            #endregion

            // 存储更新计时器结束
            sw.Stop();

            _logger.LogInformation("存储更新运行结束，耗时 {StorageUpdateTime} ms", GetMilliSeconds(sw.ElapsedTicks));

            await db.DisposeAsync();
            scope.Dispose();
        }
        catch (Exception e)
        {
            _logger.LogError("更新出现错误：{Exception}", e);
        }
        finally
        {
            if (tmpFolder.Exists)
            {
                tmpFolder.Delete(true);
            }
        }
    }

    private static bool OptionValidation(IOptions<UpdaterOption> option)
    {
        return option.Value?.Method is "Download" or "Clone";
    }

    private void ReCreateRepoDirectory()
    {
        if (_repoDirectory.Exists)
        {
            _repoDirectory.Delete(true);
        }

        _repoDirectory.Create();
    }

    private async Task<List<BlobFileInfo>> GetLocalBlobs(string path)
    {
        var realPath = Path.Combine(_repoDirectory.FullName, path);
        var files = Directory.GetFiles(realPath);
        var blobs = new List<BlobFileInfo>();

        foreach (var file in files)
        {
            var key = file.Replace(_repoDirectory.FullName, string.Empty).TrimStart('\\', '/');
            var stream = File.OpenRead(file);
            var md5 = await stream.GetMd5();
            await stream.DisposeAsync();
            blobs.Add(new BlobFileInfo(key, md5));
        }

        return blobs;
    }

    private static string GetMilliSeconds(long ticks)
    {
        return ((double)ticks / (TimeSpan.TicksPerMillisecond * 100)).ToString("F5");
    }
}
