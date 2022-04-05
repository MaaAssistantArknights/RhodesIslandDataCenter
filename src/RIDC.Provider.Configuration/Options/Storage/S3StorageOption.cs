namespace RIDC.Provider.Configuration.Options.Storage;

public record S3StorageOption
{
    public string Endpoint { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public bool UseSsl { get; set; }
    public string Bucket { get; set; }
}
