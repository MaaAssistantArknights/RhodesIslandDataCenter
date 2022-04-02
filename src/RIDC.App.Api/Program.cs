using System.Reflection;
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
    var assemblyPathConfigurationFile = Path.Combine(assemblyPath, "appsettings.json");
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
    configurationBuilder.AddJsonFile(Path.Combine(assemblyPath, "appsettings.Development.json"), true);
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
builder.Services.AddDbContext<RhodesIslandDbContextBase, RhodesIslandDbContext>();
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

#endregion

#region Pipeline

var app = builder.Build();

app.UseCors();
app.MapControllers();
app.Run();

#endregion
