using Microsoft.Extensions.DependencyInjection;
using RIDC.Provider.Configuration;
using RIDC.Provider.Configuration.Options;
using RIDC.Storage.Base;
using RIDC.Storage.S3;

namespace RIDC.Provider.Storage;

public static class StorageExtension
{
    public static IServiceCollection AddStorage(this IServiceCollection serviceCollection, RidcConfigurationProvider configurationProvider)
    {
        var storageOption = configurationProvider.GetOption<StorageOption>();
        var parsed = Enum.TryParse<StorageType>(storageOption.Type, out var storageType);
        if (parsed is false)
        {
            throw new ArgumentOutOfRangeException(nameof(configurationProvider), storageType, $"未知的存储类型 {storageOption.Type}");
        }
        switch (storageType)
        {
            case StorageType.S3:
                serviceCollection.AddScoped<IRidcStorage, S3Storage>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(configurationProvider), storageType, $"未知的存储类型 {storageOption.Type}");
        }

        return serviceCollection;
    }
}
