namespace RIDC.Provider.Configuration.Options;

public record DatabaseOption
{
    public string Type { get; set; }
    public string ConnectionString { get; set; }
    public string MySqlMariaDbVersion { get; set; }
}
