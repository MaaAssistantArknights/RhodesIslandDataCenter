using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace RIDC.Database.Postgres;

public class RhodesIslandDbContext : RhodesIslandDbContextBase
{
    private readonly string _pgSqlConnectionString;
    private readonly Action<NpgsqlDbContextOptionsBuilder> _pgOptionsAction;

    public RhodesIslandDbContext(IConfiguration configuration)
    {
        _pgSqlConnectionString = configuration["Database:ConnectionString"];
        _pgOptionsAction = null;
    }

    public RhodesIslandDbContext(string pgSqlConnectionString, Action<NpgsqlDbContextOptionsBuilder> pgOptionsAction = null)
    {
        _pgSqlConnectionString = pgSqlConnectionString;
        _pgOptionsAction = pgOptionsAction;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_pgSqlConnectionString, _pgOptionsAction);

        base.OnConfiguring(optionsBuilder);
    }
}
