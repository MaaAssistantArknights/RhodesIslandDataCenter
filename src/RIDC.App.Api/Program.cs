using RIDC.App.Api.GraphQL;
using RIDC.App.Api.Middlewares;
using RIDC.Database;
using RIDC.Provider.Configuration;
using RIDC.Provider.Configuration.Options;
using RIDC.Provider.Database;
using RIDC.Provider.Logger;
using Serilog;

var appName = Environment.GetEnvironmentVariable("RIDC_API_APP_NAME");
if (string.IsNullOrEmpty(appName))
{
    appName = "ridc-app-api";
}

#region Logger

Log.Logger = SerilogProvider.GetLogger(RidcConfigurationProvider.GetProvider(), appName);

Log.Logger.Information("启动中...");

#endregion

#region Web Application Builder

var builder = WebApplication.CreateBuilder(args);

if (RidcConfigurationProvider.IsInsideDocker() is false)
{
    var apiOption = RidcConfigurationProvider.GetProvider().GetOption<ApiOption>();
    var url = $"http://{apiOption.Host}:{apiOption.Port}";
    builder.WebHost.UseUrls(url);
}

builder.Host.UseSerilog();

builder.Configuration.AddRidcConfigurations();

builder.WebHost.ConfigureKestrel(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.AddRidcOptions();
builder.Services.AddRidcDbContextPoolFactory(RidcConfigurationProvider.GetProvider());

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
    .RegisterDbContext<RhodesIslandDbContext>(DbContextKind.Pooled)
    .AddQueryType<Query>()
    .AddFiltering()
    .AddSorting();

#endregion

#region Pipeline

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseCors();
app.MapControllers();
app.MapGraphQL();
app.Run();

#endregion
