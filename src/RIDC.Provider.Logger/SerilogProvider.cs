using RIDC.Provider.Configuration;
using Serilog;

namespace RIDC.Provider.Logger;

public static class SerilogProvider
{
    private static Serilog.Core.Logger? s_logger;

    public static Serilog.Core.Logger GetLogger(RidcConfigurationProvider configurationProvider)
    {
        return s_logger ??= new LoggerConfiguration()
            .ReadFrom.Configuration(configurationProvider.GetConfiguration())
            .CreateLogger();
    }
}
