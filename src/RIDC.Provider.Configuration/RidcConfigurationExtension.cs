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
        serviceProvider.AddOptions<DatabaseOption>().BindConfiguration("Database");
        serviceProvider.AddOptions<UpdateOption>().BindConfiguration("Update");
        return serviceProvider;
    }
}
