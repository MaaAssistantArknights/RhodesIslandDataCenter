namespace RIDC.Provider.Configuration.Options;

public record UpdaterOption
{
    public string UserAgent { get; set; }
    public string Proxy { get; set; }
    public int Interval { get; set; }
    public string Method { get; set; }
    public bool HaveStorage { get; set; }
}
