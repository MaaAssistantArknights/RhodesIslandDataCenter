using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using RIDC.Database;
using RIDC.Provider.Configuration;
using RIDC.Provider.Configuration.Options;

namespace RIDC.Provider.Database;

public static class RidcDbContextProvider
{
    public static IServiceCollection AddRidcDbContext(this IServiceCollection serviceCollection, RidcConfigurationProvider configurationProvider)
    {
        var dbOption = configurationProvider.GetOption<DatabaseOption>("Database");

        serviceCollection.AddDbContext<RhodesIslandDbContext>(options => options.SelectDatabase(dbOption));

        return serviceCollection;
    }

    public static IServiceCollection AddRidcDbContextPoolFactory(this IServiceCollection serviceCollection, RidcConfigurationProvider configurationProvider)
    {
        var dbOption = configurationProvider.GetOption<DatabaseOption>("Database");

        serviceCollection.AddPooledDbContextFactory<RhodesIslandDbContext>(options => options.SelectDatabase(dbOption));

        return serviceCollection;
    }

    public static void AddRidcDbContextDesignTime(this IServiceCollection serviceCollection, string provider, string connectionString)
    {
        var dbOption = new DatabaseOption { Type = provider, ConnectionString = connectionString };
        serviceCollection.AddDbContext<RhodesIslandDbContext>(options => options.SelectDatabase(dbOption));
    }

    private static void SelectDatabase(this DbContextOptionsBuilder options, DatabaseOption dbOption)
    {
        switch (dbOption.Type)
        {
            case "Postgres":
                options.UseNpgsql(dbOption.ConnectionString);
                break;
            case "MySql":
                options.UseMySql(dbOption.ConnectionString, ServerVersion.Create(
                    Version.Parse(dbOption.MySqlMariaDbVersion), ServerType.MySql));
                break;
            case "MariaDb":
                options.UseMySql(dbOption.ConnectionString, ServerVersion.Create(
                    Version.Parse(dbOption.MySqlMariaDbVersion), ServerType.MariaDb));
                break;
            case "Sqlite":
                options.UseSqlite(dbOption.ConnectionString);
                break;
            default:
                throw new Exception("未知的数据库类型");
        }
    }
}
