using RIDC.App.Api.GraphQL;
using RIDC.App.Api.Middlewares;
using RIDC.Database;
using RIDC.Provider.Configuration;
using RIDC.Provider.Database;
using Serilog;

#region Logger

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(RidcConfigurationProvider.GetProvider().GetConfiguration())
    .CreateLogger();

Log.Logger.Information("启动中...");

#endregion

#region Web Application Builder

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Configuration.AddRidcConfigurations();

builder.WebHost.ConfigureKestrel(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.AddRidcOptions();
builder.Services.AddRidcDbContext(RidcConfigurationProvider.GetProvider());

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
    .RegisterDbContext<RhodesIslandDbContext>()
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
