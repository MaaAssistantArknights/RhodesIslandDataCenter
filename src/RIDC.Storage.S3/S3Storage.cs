using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Tags;
using RIDC.Provider.Configuration.Options;
using RIDC.Provider.Configuration.Options.Storage;
using RIDC.Storage.Base;

namespace RIDC.Storage.S3;

public class S3Storage : IRidcStorage
{
    private readonly ILogger<S3Storage> _logger;

    private readonly MinioClient _s3;
    private readonly string _bucketName;

    private const string Policy =
        "{\"Version\":\"2012-10-17\",\"Statement\":[{\"Sid\":\"PublicRead\",\"Effect\":\"Allow\",\"Principal\":\"*\",\"Action\":[\"s3:GetObject\",\"s3:GetObjectVersion\"],\"Resource\":[\"arn:aws:s3:::{BUCKET_NAME}/*\"]}]}";

    public S3Storage(IOptions<S3StorageOption> s3Option, IOptions<StorageOption> storageOption, ILogger<S3Storage> logger)
    {
        _logger = logger;
        _bucketName = s3Option.Value.Bucket;
        var s3ClientBuilder = new MinioClient()
            .WithEndpoint(s3Option.Value.Endpoint)
            .WithCredentials(s3Option.Value.AccessKey, s3Option.Value.SecretKey);
        if (s3Option.Value.UseSsl)
        {
            s3ClientBuilder.WithSSL();
        }
        _s3 = s3ClientBuilder.Build();

        var bucketExist = _s3.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName)).Result;

        if (bucketExist is not false)
        {
            return;
        }

        _s3.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName)).Wait();

        if (storageOption.Value.Invariant is "Minio" or "Amazon S3")
        {
            var policy = Policy.Replace("{BUCKET_NAME}", _bucketName);
            _s3.SetPolicyAsync(new SetPolicyArgs().WithBucket(_bucketName).WithPolicy(policy)).Wait();
        }
        else
        {
            _logger.LogWarning("使用非 Minio 或 Amazon S3 存储服务，请自行设置存储桶权限");
        }
    }

    public async Task<List<BlobFileInfo>> GetBlobsAsync(string remoteDirectoryPath)
    {
        try
        {
            var items = new List<BlobFileInfo>();
            var observable = _s3.ListObjectsAsync(new ListObjectsArgs()
                .WithBucket(_bucketName)
                .WithPrefix(remoteDirectoryPath));
            var finished = false;
            var subscription = observable.Subscribe(
                item =>
                {
                    var detailItem = _s3.GetObjectTagsAsync(
                        new GetObjectTagsArgs()
                            .WithBucket(_bucketName)
                            .WithObject(item.Key)).Result;
                    detailItem.GetTags().TryGetValue("MD5", out var md5);
                    items.Add(new BlobFileInfo(item.Key, md5));
                },
                ex => throw ex,
                () =>
                {
                    _logger.LogInformation("已获取 S3 路径 {StorageRemoteDir} 中的 {StorageGetFiles} 个文件信息",
                        remoteDirectoryPath, items.Count);
                    finished = true;
                });

            while (finished is false)
            {
                await Task.Delay(100);
            }

            subscription.Dispose();
            return items;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "列出 S3 Blobs 时出现错误，{StorageErrorType}", StorageErrorType.FailedToListObjects.ToString());
            return null;
        }
    }

    public async Task<bool> UploadBlobsAsync(IDictionary<string, Stream> fileList)
    {
        try
        {
            foreach (var (path, stream) in fileList)
            {
                var md5 = await stream.GetMd5();
                await _s3.PutObjectAsync(new PutObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(path)
                    .WithStreamData(stream)
                    .WithObjectSize(stream.Length)
                    .WithTagging(new Tagging(new Dictionary<string, string> { { "MD5", md5 } }, false)));
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "上传 S3 Blob 出错，{StorageErrorType}", StorageErrorType.FailedToUploadObject.ToString());
            return false;
        }
    }

    public async Task<bool> DeleteBlobsAsync(string[] remoteFilePath)
    {
        try
        {
            foreach (var remoteFile in remoteFilePath)
            {
                await _s3.RemoveObjectAsync(new RemoveObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(remoteFile));
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "删除 S3 Blob 出错，{StorageErrorType}", StorageErrorType.FailedToDeleteObject.ToString());
            return false;
        }
    }

    public async Task<string> GetBlobHashAsync(string remoteFilePath)
    {
        try
        {
            var tags = await _s3.GetObjectTagsAsync(new GetObjectTagsArgs()
                .WithBucket(_bucketName)
                .WithObject(remoteFilePath));
            tags.GetTags().TryGetValue("MD5", out var md5);
            return md5;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取 S3 Blob Tags 出错，{StorageErrorType}", StorageErrorType.FailedToGetObjectTags.ToString());
            return null;
        }
    }
}
