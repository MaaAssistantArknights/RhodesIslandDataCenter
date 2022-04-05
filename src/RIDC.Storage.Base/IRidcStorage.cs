namespace RIDC.Storage.Base;

public interface IRidcStorage
{
    /// <summary>
    /// 获取远程路径下的所有文件信息
    /// </summary>
    /// <param name="remoteDirectoryPath"></param>
    /// <returns></returns>
    Task<List<BlobFileInfo>> GetBlobsAsync(string remoteDirectoryPath);

    /// <summary>
    /// 上传本地文件至远程路径
    /// </summary>
    /// <param name="fileList"></param>
    /// <returns></returns>
    Task<bool> UploadBlobsAsync(IDictionary<string, Stream> fileList);

    /// <summary>
    /// 删除远程文件
    /// </summary>
    /// <param name="remoteFilePath"></param>
    /// <returns></returns>
    Task<bool> DeleteBlobsAsync(string[] remoteFilePath);

    /// <summary>
    /// 获取一个远程文件的哈希值
    /// </summary>
    /// <param name="remoteFilePath"></param>
    /// <returns></returns>
    Task<string> GetBlobHashAsync(string remoteFilePath);
}
