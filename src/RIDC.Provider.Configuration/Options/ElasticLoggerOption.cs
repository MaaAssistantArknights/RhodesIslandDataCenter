namespace RIDC.Provider.Configuration.Options;

public record ElasticLoggerOption
{
    public bool EnableElasticSearch { get; set; }
    public string ElasticSearchUri { get; set; }
    public string Id { get; set; }
    public string Key { get; set; }
}
