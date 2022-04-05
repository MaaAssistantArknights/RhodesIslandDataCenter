using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RIDC.Provider.Configuration.Options;
using RIDC.Provider.Configuration.Options.Storage;

namespace RIDC.Provider.Configuration;

public static class RidcConfigurationExtension
{
    public static IConfigurationBuilder AddRidcConfigurations(this IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddConfiguration(RidcConfigurationProvider.GetProvider().GetConfiguration());
        return configurationBuilder;
    }

    public static IServiceCollection AddRidcOptions(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddOptions<DatabaseOption>().BindConfiguration(nameof(DatabaseOption).FormatOptionSection());
        serviceProvider.AddOptions<UpdaterOption>().BindConfiguration(nameof(UpdaterOption).FormatOptionSection());
        serviceProvider.AddOptions<ApiOption>().BindConfiguration(nameof(ApiOption).FormatOptionSection());
        serviceProvider.AddOptions<ElasticLoggerOption>().BindConfiguration(nameof(ElasticLoggerOption).FormatOptionSection());
        serviceProvider.AddOptions<StorageOption>().BindConfiguration(nameof(StorageOption).FormatOptionSection());

        serviceProvider.AddOptions<S3StorageOption>().BindConfiguration(nameof(S3StorageOption).FormatOptionSection());

        return serviceProvider;
    }

    internal static string FormatOptionSection(this string name)
    {
        var section = name.Replace("Option", string.Empty);
        if (!section.EndsWith("Storage") || section.StartsWith("Storage") is not false)
        {
            return section;
        }

        section = section.Replace("Storage", string.Empty);
        return "Storage:" + section;
    }
}
