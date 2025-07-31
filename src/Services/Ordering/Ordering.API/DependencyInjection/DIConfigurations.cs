using BuildingBlocks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
namespace Ordering.API.DependencyInjection;
public static class DIConfigurations
{
    public static IServiceCollection AddOrderingWebServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var assembly = typeof(Program).Assembly;

        var sqlConnectionString = KeyVaultConfigLoader.LoadSecret(configuration, "AzureSQLDatabaseConnection", "VaultUri");

        services.AddCarter();
        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddHealthChecks().AddSqlServer(sqlConnectionString);

        return services;
    }

    public static WebApplication UseOrderingAPIServices(this WebApplication app)
    {
        //app.UseMarten();
        app.MapCarter();

        app.UseExceptionHandler(options => { });

        app.UseHealthChecks("/health",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        return app;
    }
}
