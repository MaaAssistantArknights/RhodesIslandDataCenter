namespace RIDC.Provider.Configuration.Options;

public record UpdateOption
{
    public string Proxy { get; set; }
    public int Interval { get; set; }
}
