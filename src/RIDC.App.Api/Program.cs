using System.Reflection;
using RIDC.App.Api.GraphQL;
using RIDC.Database.MySql;
using RIDC.Database.Postgres;
using Serilog;

#region Find Configuration File

var configurationFileEnvironmentVariable = Environment.GetEnvironmentVariable("RIDC_CONFIGURATION_FILE");
var assemblyPath = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory!.FullName;

string configFile;
if (File.Exists(configurationFileEnvironmentVariable))
{
    configFile = configurationFileEnvironmentVariable;
}
else
{
    var assemblyPathConfigurationFile = System.IO.Path.Combine(assemblyPath, "appsettings.json");
    if (File.Exists(assemblyPathConfigurationFile))
    {
        configFile = assemblyPathConfigurationFile;
    }
    else
    {
        Console.Error.WriteLine("找不到配置文件");
        return;
    }
}

#endregion

#region Configuraion And Logger

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile(configFile);

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    configurationBuilder.AddJsonFile(System.IO.Path.Combine(assemblyPath, "appsettings.Development.json"), true);
}

configurationBuilder
    .AddEnvironmentVariables("RIDC:")
    .AddCommandLine(args);

var configuration = configurationBuilder.Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

Log.Logger.Information("启动中...");

#endregion

#region Web Application Builder

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Configuration.AddConfiguration(configuration);

switch (configuration["Database:Type"])
{
    case "Postgres":
        builder.Services.AddDbContext<RhodesIslandDbContextBase, RhodesIslandDbContextPostgres>();
        break;
    case "MySql":
        builder.Services.AddDbContext<RhodesIslandDbContextBase, RhodesIslandDbContextMySql>();
        break;
    default:
        Log.Fatal("未知的数据库类型");
        return;
}

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});
builder.Services.AddGraphQLServer()
    .RegisterDbContext<RhodesIslandDbContextBase>()
    .AddQueryType<Query>()
    .AddFiltering()
    .AddSorting();

#endregion

#region Pipeline

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseCors();
app.MapControllers();
app.MapGraphQL();
app.Run();

#endregion
