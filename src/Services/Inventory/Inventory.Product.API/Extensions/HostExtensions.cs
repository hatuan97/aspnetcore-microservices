using Common.Logging;
using Inventory.Product.API.Persistence;
using MongoDB.Driver;
using Serilog;
using Shared.Configurations;

namespace Inventory.Product.API.Extensions;

public static class HostExtensions
{
    internal static void AddAppConfigurations(this ConfigureHostBuilder host)
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

    public static IHost MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var settings = services.GetService<MongoDbSettings>();
        if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
            throw new ArgumentNullException("DatabaseSettings is not configured");

        var mongoClient = services.GetRequiredService<IMongoClient>();
        new InventoryDbSeed()
            .SeedDataAsync(mongoClient, settings)
            .Wait();
        return host;
    }
}