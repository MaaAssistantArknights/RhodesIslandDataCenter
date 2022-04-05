using System.Security.Cryptography;

namespace RIDC.Storage.Base;

public static class BlobHashUtility
{
    public static async Task<string> GetMd5(this Stream stream)
    {
        var md5 = MD5.Create();
        var result = await md5.ComputeHashAsync(stream);
        return BitConverter.ToString(result).Replace("-", string.Empty).ToLower();
    }
}
