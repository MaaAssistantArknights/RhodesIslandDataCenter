using Microsoft.Extensions.Configuration;
using RIDC.Provider.Configuration;
using RIDC.Provider.Configuration.Options;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace RIDC.Provider.Logger;

public static class SerilogProvider
{
    private static Serilog.Core.Logger? s_logger;

    public static Serilog.Core.Logger GetLogger(RidcConfigurationProvider configurationProvider, string appName)
    {
        if (s_logger is not null)
        {
            return s_logger;
        }

        var option = configurationProvider.GetOption<ElasticLoggerOption>();

        var loggingFileWritePath = configurationProvider.GetConfiguration().GetValue<string>("Serilog:WriteTo:1:Args:path");
        var loggerConfiguration = new ConfigurationBuilder()
            .AddConfiguration(RidcConfigurationProvider.GetProvider().GetConfiguration())
            .AddInMemoryCollection(new List<KeyValuePair<string, string>>
            {
                new("Serilog:WriteTo:1:Args:path", loggingFileWritePath.Replace("%{APP NAME PLACEHOLDER}%", appName))
            })
            .Build();

        var conf = new LoggerConfiguration()
            .ReadFrom.Configuration(loggerConfiguration);

        if (option.EnableElasticSearch)
        {
            var isAspDev = configurationProvider.GetConfiguration()
                               .GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Development";
            var isDotNetDev = configurationProvider.GetConfiguration()
                                  .GetValue<string>("DOTNET_ENVIRONMENT") == "Development";

            var env = isAspDev || isDotNetDev ? "dev" : "prod";


            conf.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(option.ElasticSearchUri))
            {
                IndexFormat = $"{appName}-logs-{env}-{DateTime.UtcNow:yyyy-MM}",
                FailureCallback = e => Console.WriteLine($"日志写入到 Elasticsearch 失败，请检查配置，{e.Exception}"),
                AutoRegisterTemplate = true,
                NumberOfShards = 2,
                NumberOfReplicas = 1,
                ModifyConnectionSettings = c => c.ApiKeyAuthentication(option.Id, option.Key)
            });
        }

        s_logger = conf.CreateLogger();

        return s_logger;
    }
}
