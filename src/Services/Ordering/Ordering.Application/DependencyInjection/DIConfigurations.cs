namespace Ordering.Application.DependencyInjection;
public static class DIConfigurations
{
    public static IServiceCollection AddOrderingApplicationServices(this IServiceCollection services)
    {
        //var assembly = typeof(Program).Assembly;
        ////services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters(); // it is Enable the integration between FluentValidation and ASP.NET MVC's automatic validation pipeline.


        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
        });
        //services.AddValidatorsFromAssembly(assembly);

        //services.AddCarter();

        //var connString = configuration.GetConnectionString("DatabaseConnection") ?? throw new InvalidOperationException("Database connection string not configured.");

        //services.AddMarten(options =>
        //{
        //    options.Connection(connString);
        //}).UseLightweightSessions();

        //services.AddHealthChecks().AddNpgSql(connString);

        return services;
    }
}
