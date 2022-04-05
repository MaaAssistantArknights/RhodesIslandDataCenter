namespace RIDC.Storage.Base;

public interface IRidcStorage
{

    /// <summary>
    /// 获取存储服务器 Blob 版本
    /// </summary>
    /// <returns></returns>
    Task<string> GetBlobVersionAsync();

    /// <summary>
    /// 获取远程路径下的所有文件信息
    /// </summary>
    /// <param name="remoteDirectoryPath"></param>
    /// <returns></returns>
    Task<List<BlobFileInfo>> GetBlobsAsync(string remoteDirectoryPath);

    /// <summary>
    /// 上传本地文件至远程路径
    /// </summary>
    /// <param name="localBlobFileInfos"></param>
    /// <returns></returns>
    Task<bool> UploadBlobsAsync(IEnumerable<LocalBlobFileInfo> localBlobFileInfos);

    /// <summary>
    /// 删除远程文件
    /// </summary>
    /// <param name="remoteFilePaths"></param>
    /// <returns></returns>
    Task<bool> DeleteBlobsAsync(IEnumerable<string> remoteFilePaths);
}
