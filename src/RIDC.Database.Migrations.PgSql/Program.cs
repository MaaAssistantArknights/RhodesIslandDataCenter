using Microsoft.Extensions.Hosting;
using RIDC.Provider.Database;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddRidcDbContextDesignTime("PgSql",
            "Server=127.0.0.1;Port=5432;Database=myDataBase;User Id=myUsername;Password=myPassword;");
    })
    .Build();


await host.RunAsync();
