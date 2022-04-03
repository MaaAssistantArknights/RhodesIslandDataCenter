using RIDC.App.Updater;
using RIDC.Provider.Configuration;
using RIDC.Provider.Database;
using Serilog;

#region Logger

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(RidcConfigurationProvider.GetProvider().GetConfiguration())
    .CreateLogger();

Log.Logger.Information("启动中...");

#endregion

#region Host Builder

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((_, builder) =>
    {
        builder.AddRidcConfigurations();
    })
    .ConfigureServices((_, services) =>
    {
        services.AddRidcDbContext(RidcConfigurationProvider.GetProvider());
        services.AddRidcOptions();
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

#endregion

await host.RunAsync();
