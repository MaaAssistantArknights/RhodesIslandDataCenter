using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace RIDC.Provider.Configuration;

public class RidcConfigurationProvider
{
    private static RidcConfigurationProvider s_ridcConfigurationProvider;
    private readonly IConfiguration _configuration;

    private RidcConfigurationProvider()
    {
        var assemblyPath = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory!.FullName;
        var dataDirectory = Path.Combine(assemblyPath, "data");

        if (Directory.Exists(dataDirectory) is false)
        {
            Directory.CreateDirectory(dataDirectory);
        }

        var configFile = Path.Combine(dataDirectory, "appsettings.json");
        if (File.Exists(configFile) is false)
        {
            File.Copy(Path.Combine(assemblyPath, "appsettings.json"), configFile);
        }

        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile(configFile, false, true)
            .AddEnvironmentVariables("RIDC_");

        if (IsDevelopment())
        {
            if (File.Exists(Path.Combine(dataDirectory, "appsettings.Development.json")) is false)
            {
                File.Copy(Path.Combine(assemblyPath, "appsettings.Development.json"),
                    Path.Combine(dataDirectory, "appsettings.Development.json"));
            }
            configurationBuilder.AddJsonFile("appsettings.Development.json", true, true);
        }

        configurationBuilder.AddInMemoryCollection(new List<KeyValuePair<string, string>>
        {
            new("AssemblyPath", assemblyPath),
            new("ConfigurationFile", configFile),
            new ("DataDirectory", dataDirectory)
        });

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

    public T GetOption<T>() where T : new()
    {
        var name = typeof(T).Name.Replace("Option", string.Empty);
        var obj = new T();
        _configuration.GetSection(name)
            .Bind(obj);
        return obj;
    }

    public static bool IsInsideDocker()
    {
        return Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") is "true";
    }

    public static bool IsDevelopment()
    {
        return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ||
            Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Development";
    }
}
