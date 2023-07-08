using Common.Logging;
using Serilog;
using Shared.Configurations;

namespace Hangfire.API.Extensions;

public static class HostExtensions
{
    public static void AddAppConfigurations(this ConfigureHostBuilder host)
    {
        host.ConfigureAppConfiguration((context, config) =>
        {
            var env = context.HostingEnvironment;
            config.AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
        }).UseSerilog(Serilogger.Configure);
    }

    internal static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder app, IConfiguration configuration)
    {
        var configDashboard = configuration.GetSection("HangFireSettings:Dashboard").Get<DashboardOptions>();
        var hangfireSettings = configuration.GetSection("HangFireSettings").Get<HangFireSettings>();
        var hangfireRoute = hangfireSettings.Route;

        app.UseHangfireDashboard(hangfireRoute, new DashboardOptions
        {
            Authorization = new[] { new AuthorizationFilter() },
            DashboardTitle = configDashboard.DashboardTitle,
            StatsPollingInterval = configDashboard.StatsPollingInterval,
            AppPath = configDashboard.AppPath,
            IgnoreAntiforgeryToken = true
        });

        return app;
    }
}