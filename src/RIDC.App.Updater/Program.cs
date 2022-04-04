using RIDC.App.Updater;
using RIDC.Provider.Configuration;
using RIDC.Provider.Database;
using RIDC.Provider.Logger;
using Serilog;

var appName = Environment.GetEnvironmentVariable("RIDC_UPDATER_APP_NAME");
if (string.IsNullOrEmpty(appName))
{
    appName = "ridc-app-api";
}

#region Logger

Log.Logger = SerilogProvider.GetLogger(RidcConfigurationProvider.GetProvider(), appName);

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
