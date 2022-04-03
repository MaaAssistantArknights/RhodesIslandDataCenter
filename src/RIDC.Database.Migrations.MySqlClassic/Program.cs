using Microsoft.Extensions.Hosting;
using RIDC.Provider.Database;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddRidcDbContextDesignTime("MySqlClassic",
            "Server=myServerAddress;Port=1234;Database=myDataBase;Uid=myUsername;Pwd=myPassword;");
    })
    .Build();


await host.RunAsync();
