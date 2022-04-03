using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace RIDC.Provider.Configuration;

public class RidcConfigurationProvider
{
    private static RidcConfigurationProvider s_ridcConfigurationProvider;
    private readonly IConfiguration _configuration;

    private RidcConfigurationProvider()
    {
        var configurationFileEnvironmentVariable = Environment.GetEnvironmentVariable("RIDC_API_CONFIGURATION");
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

        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile(configFile, false, true)
            .AddEnvironmentVariables("RIDC_");

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ||
            Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Development")
        {
            configurationBuilder.AddJsonFile("appsettings.Development.json", true, true);
        }

        _configuration = configurationBuilder.Build();
    }

    public static RidcConfigurationProvider GetProvider()
    {
        return s_ridcConfigurationProvider ??= new RidcConfigurationProvider();
    }

    public IConfiguration GetConfiguration()
    {
        return _configuration;
    }

    public T GetOption<T>(string section) where T : new()
    {
        var obj = new T();
        _configuration.GetSection(section)
            .Bind(obj);
        return obj;
    }
}
