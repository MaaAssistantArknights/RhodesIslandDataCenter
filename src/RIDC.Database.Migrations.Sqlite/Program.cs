using Microsoft.Extensions.Hosting;
using RIDC.Provider.Database;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddRidcDbContextDesignTime("Sqlite",
            "Data Source=:memory:;Version=3;New=True;");
    })
    .Build();


await host.RunAsync();
