namespace RIDC.Storage.Base;

public interface IRidcStorage
{
    /// <summary>
    /// 获取远程路径下的所有文件信息
    /// </summary>
    /// <param name="remoteDirectoryPath"></param>
    /// <returns></returns>
    Task<List<BlobFileInfo>> GetFilesAsync(string remoteDirectoryPath);

    /// <summary>
    /// 上传本地文件至远程路径
    /// </summary>
    /// <param name="remoteFilePath"></param>
    /// <param name="localFileStream"></param>
    /// <returns></returns>
    Task<bool> UploadFileAsync(string remoteFilePath, Stream localFileStream);

    /// <summary>
    /// 删除一个远程文件
    /// </summary>
    /// <param name="remoteFilePath"></param>
    /// <returns></returns>
    Task<bool> DeleteFileAsync(string remoteFilePath);

    /// <summary>
    /// 获取一个远程文件的哈希值
    /// </summary>
    /// <param name="remoteFilePath"></param>
    /// <returns></returns>
    Task<string> GetFileHashAsync(string remoteFilePath);
}
