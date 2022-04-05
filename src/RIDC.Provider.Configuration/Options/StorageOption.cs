namespace RIDC.Provider.Configuration.Options;

public record StorageOption
{
    public string Type { get; set; }
    public string Invariant { get; set; }
}
