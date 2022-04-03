using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.EntityFrameworkCore.Infrastructure;

namespace RIDC.Database.MySql;

public class RhodesIslandDbContextMySql : RhodesIslandDbContextBase
{
    private readonly string _mySqlConnectionString;
    private readonly Action<MySQLDbContextOptionsBuilder>? _mySqlOptionsAction;

    public RhodesIslandDbContextMySql(IConfiguration configuration)
    {
        _mySqlConnectionString = configuration["Database:ConnectionString"];
        _mySqlOptionsAction = null;
    }

    public RhodesIslandDbContextMySql(string pgSqlConnectionString,
        Action<MySQLDbContextOptionsBuilder>? pgOptionsAction = null)
    {
        _mySqlConnectionString = pgSqlConnectionString;
        _mySqlOptionsAction = pgOptionsAction;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(_mySqlConnectionString, _mySqlOptionsAction);

        base.OnConfiguring(optionsBuilder);
    }
}
