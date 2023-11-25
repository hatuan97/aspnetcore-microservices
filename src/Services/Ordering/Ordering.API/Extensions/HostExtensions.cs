using Common.Logging;
using Serilog;

namespace Ordering.API.Extensions;

public static class HostExtensions
{
    public static void AddAppConfigurations(this ConfigureHostBuilder host)
    {
        host.ConfigureAppConfiguration((context, config) =>
        {
            var env = context.HostingEnvironment;
            config.AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true,
                    true)
                .AddEnvironmentVariables();
        }).UseSerilog(Serilogger.Configure);
    }
}