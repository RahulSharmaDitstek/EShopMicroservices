using JasperFx;

namespace Catalog.API.DependencyInjection;
public static class DIConfigurations
{
    public static IServiceCollection AddCatalogServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddCarter();

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        var connString = configuration.GetConnectionString("DatabaseConnection");
        services.AddMarten(options =>
        {
            options.Connection(connString);
        }).UseLightweightSessions();

        return services;
    }
}
