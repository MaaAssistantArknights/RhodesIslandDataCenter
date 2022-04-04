using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using RIDC.Database;
using RIDC.Provider.Configuration;
using RIDC.Provider.Configuration.Options;

namespace RIDC.Provider.Database;

public static class RidcDbContextProvider
{
    public static void UpdateDatabase(this RhodesIslandDbContext db, ILogger logger = null)
    {
        // ReSharper disable once ConvertToLocalFunction
        var log = (string message) =>
        {
            if (logger is null)
            {
                Console.WriteLine(message);
            }
            else
            {
                logger.LogInformation("{Message}", message);
            }
        };

        var pendingMigrations = db.Database.GetPendingMigrations().ToArray();
        if (!pendingMigrations.Any())
        {
            log("数据库架构已是最新");
            return;
        }

        log($"应用数据库迁移：{string.Join("，", pendingMigrations)}");
        db.Database.Migrate();
    }

    public static IServiceCollection AddRidcDbContext(this IServiceCollection serviceCollection, RidcConfigurationProvider configurationProvider)
    {
        var dbOption = configurationProvider.GetOption<DatabaseOption>();

        serviceCollection.AddDbContext<RhodesIslandDbContext>(options => options.SelectDatabase(dbOption));

        return serviceCollection;
    }

    public static IServiceCollection AddRidcDbContextPoolFactory(this IServiceCollection serviceCollection, RidcConfigurationProvider configurationProvider)
    {
        var dbOption = configurationProvider.GetOption<DatabaseOption>();

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
            case "PgSql":
                options.UseNpgsql(dbOption.ConnectionString,
                    o => o.MigrationsAssembly("RIDC.Database.Migrations.PgSql"));
                break;
            case "MySql":
                options.UseMySql(dbOption.ConnectionString, ServerVersion.Create(
                    new Version(8, 0), ServerType.MySql),
                    o => o.MigrationsAssembly("RIDC.Database.Migrations.MySql"));
                break;
            case "MySqlClassic":
                options.UseMySql(dbOption.ConnectionString, ServerVersion.Create(
                        new Version(5, 7), ServerType.MySql),
                    o => o.MigrationsAssembly("RIDC.Database.Migrations.MySqlClassic"));
                break;
            case "MariaDb":
                options.UseMySql(dbOption.ConnectionString, ServerVersion.Create(
                    new Version(10, 8), ServerType.MariaDb),
                    o => o.MigrationsAssembly("RIDC.Database.Migrations.MariaDb"));
                break;
            case "Sqlite":
                options.UseSqlite(dbOption.ConnectionString,
                    o => o.MigrationsAssembly("RIDC.Database.Migrations.Sqlite"));
                break;
            default:
                throw new Exception("未知的数据库类型");
        }
    }
}
