using Ordering.Application.Data;

namespace Ordering.Infrastructure.DependencyInjection;
public static class DIConfigurations
{
    public static IServiceCollection AddOrderingInfrastructureServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("AzureSQLDatabaseConnection") ?? throw new InvalidOperationException("Azure SQL Database connection string not configured.");

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
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IApplicationDbContext, OrderingDbContext>();
        return services;
    }
}
