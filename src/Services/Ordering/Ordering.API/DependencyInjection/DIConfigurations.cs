namespace Ordering.API.DependencyInjection;
public static class DIConfigurations
{
    public static IServiceCollection AddOrderingWebServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var assembly = typeof(Program).Assembly;
        var sqlConnectionString = configuration.GetConnectionString("AzureSQLDatabaseConnection") ?? throw new InvalidOperationException("Azure SQL Database connection string not configured.");

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

        app.UseHealthChecks("/health");
        return app;
    }
}
