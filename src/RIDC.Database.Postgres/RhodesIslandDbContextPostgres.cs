using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace RIDC.Database.Postgres;

public class RhodesIslandDbContextPostgres : RhodesIslandDbContextBase
{
    private readonly string _pgSqlConnectionString;
    private readonly Action<NpgsqlDbContextOptionsBuilder> _pgOptionsAction;

    public RhodesIslandDbContextPostgres(IConfiguration configuration)
    {
        _pgSqlConnectionString = configuration["Database:ConnectionString"];
        _pgOptionsAction = null;
    }

    public RhodesIslandDbContextPostgres(string pgSqlConnectionString, Action<NpgsqlDbContextOptionsBuilder> pgOptionsAction = null)
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
