using BuildingBlocks;

namespace Ordering.Infrastructure.DependencyInjection;
public static class DIConfigurations
{
    public static IServiceCollection AddOrderingInfrastructureServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var sqlConnectionString = KeyVaultConfigLoader.LoadSecret(configuration, "AzureSQLDatabaseConnection", "VaultUri");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();

        //services.AddHealthChecks().AddNpgSql(connString);
        services.AddDbContext<OrderingDbContext>((sp, options) =>
        {
            var interceptors = sp.GetServices<ISaveChangesInterceptor>().ToArray();
            if (interceptors.Length > 0)
            {
                options.AddInterceptors(interceptors);
            }
            options.UseSqlServer(sqlConnectionString);
        });
        services.AddScoped<IApplicationDbContext, OrderingDbContext>();
        return services;
    }
}
