using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RIDC.Provider.Configuration.Options;

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
        serviceProvider.AddOptions<DatabaseOption>().BindConfiguration(nameof(DatabaseOption).Replace("Option", string.Empty));
        serviceProvider.AddOptions<UpdaterOption>().BindConfiguration(nameof(UpdaterOption).Replace("Option", string.Empty));
        serviceProvider.AddOptions<ApiOption>().BindConfiguration(nameof(ApiOption).Replace("Option", string.Empty));
        serviceProvider.AddOptions<ElasticLoggerOption>().BindConfiguration(nameof(ElasticLoggerOption).Replace("Option", string.Empty));
        return serviceProvider;
    }
}
