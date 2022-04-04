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
using RIDC.Schema.Comparer;
using RIDC.Schema.Json;
using RIDC.Schema.Json.Mapper;

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
            var sw = new Stopwatch();
            sw.Start();

            using var scope = _serviceProvider.CreateScope();
            var option = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<UpdaterOption>>();
            if (OptionValidation(option) is false)
            {
                throw new Exception("更新配置错误");
            }

            await using var db = scope.ServiceProvider.GetRequiredService<RhodesIslandDbContext>();
            db.UpdateDatabase(_logger);
            var proxy = string.IsNullOrEmpty(option.Value.Proxy) ? null : new WebProxy(option.Value.Proxy);
            using var client = new HttpClient(new HttpClientHandler
            {
                Proxy = proxy, UseProxy = proxy is not null, AllowAutoRedirect = true
            });
            client.DefaultRequestHeaders.Add("User-Agent", option.Value.UserAgent);

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

            var inStoreVersion = db.Miscellaneous.FirstOrDefault(x => x.Key == "version");
            if (inStoreVersion?.Value == versionString)
            {
                _logger.LogInformation("版本无变化，{LocalVersion}", inStoreVersion.Value);
                return;
            }

            _logger.LogInformation("版本更新：{LocalVersion} -> {GitHubVersion}", inStoreVersion, versionString);

            #endregion

            #region Get Repo

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

            #endregion

            #region Read Files

            var jsonCharacterStr =
                await File.ReadAllTextAsync(
                    Path.Combine(_repoDirectory.FullName, "gamedata/excel/character_table.json"), stoppingToken);
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

            #region Character

            var characterArray = JsonDocument.Parse(jsonCharacterStr)
                .RootElement
                .EnumerateObject();
            var characters = characterArray
                .Select(x => x.Value
                    .Deserialize<JsonCharacter>()
                    .ToCharacter(x.Name, powers, skills))
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

            #endregion Deserialize Data

            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            #region Query Database

            var dbPowers = await db.Powers.ToListAsync(stoppingToken);
            var dbSkills = await db.Skills.ToListAsync(stoppingToken);
            var dbCharacters = await db.Characters
                .Include(x => x.Skills)
                .Include(x => x.Nation)
                .ToListAsync(stoppingToken);
            var dbStages = await db.Stages.ToListAsync(stoppingToken);
            var dbZones = await db.Zones
                .Include(x => x.Stages)
                .ToListAsync(stoppingToken);
            var dbCharms = await db.Charms.ToListAsync(stoppingToken);
            var dbItems = await db.Items.ToListAsync(stoppingToken);
            var dbTips = await db.Tips.ToListAsync(stoppingToken);

            #endregion

            #region Object Compare

            dbPowers.Compare(powers, x => x.PowerId, out var modifiedPower, out var deletedPower, out var addedPower);
            dbSkills.Compare(skills, x => x.SkillId, out var modifiedSkill, out var deletedSkill, out var addedSkill);
            dbCharacters.Compare(characters, x => x.CharacterId, out var modifiedCharacter, out var deletedCharacter, out var addedCharacter);
            dbStages.Compare(stages, x => x.StageId, out var modifiedStage, out var deletedStage, out var addedStage);
            dbZones.Compare(zones, x => x.ZoneId, out var modifiedZone, out var deletedZone, out var addedZone);
            dbCharms.Compare(charms, x => x.CharmId, out var modifiedCharm, out var deletedCharm, out var addedCharm);
            dbItems.Compare(items, x => x.ItemId, out var modifiedItem, out var deletedItem, out var addedItem);
            dbTips.Compare(tips, x => x.TipId, out var modifiedTip, out var deletedTip, out var addedTip);

            #endregion

            #region Database Update

            db.Powers.UpdateRange(modifiedPower);
            db.Powers.RemoveRange(deletedPower);
            db.Powers.AddRange(addedPower);
            db.Skills.UpdateRange(modifiedSkill);
            db.Skills.RemoveRange(deletedSkill);
            db.Skills.AddRange(addedSkill);
            db.Characters.UpdateRange(modifiedCharacter);
            db.Characters.RemoveRange(deletedCharacter);
            db.Characters.AddRange(addedCharacter);
            db.Stages.UpdateRange(modifiedStage);
            db.Stages.RemoveRange(deletedStage);
            db.Stages.AddRange(addedStage);
            db.Zones.UpdateRange(modifiedZone);
            db.Zones.RemoveRange(deletedZone);
            db.Zones.AddRange(addedZone);
            db.Charms.UpdateRange(modifiedCharm);
            db.Charms.RemoveRange(deletedCharm);
            db.Charms.AddRange(addedCharm);
            db.Items.UpdateRange(modifiedItem);
            db.Items.RemoveRange(deletedItem);
            db.Items.AddRange(addedItem);
            db.Tips.UpdateRange(modifiedTip);
            db.Tips.RemoveRange(deletedTip);
            db.Tips.AddRange(addedTip);

            #endregion

            #region Set Database Version

            if (inStoreVersion is null)
            {
                db.Miscellaneous.Add(new Miscellaneous { Key = "version", Value = versionString });
            }
            else
            {
                inStoreVersion.Value = versionString;
                db.Miscellaneous.Update(inStoreVersion);
            }

            #endregion

            var changes = await db.SaveChangesAsync(stoppingToken);

            await db.DisposeAsync();
            scope.Dispose();

            sw.Stop();

            _logger.LogInformation("更新运行结束，耗时 {UpdateTime} ms，{UpdateDbChanges} 条数据库修改记录",
                sw.ElapsedMilliseconds, changes);
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
}
