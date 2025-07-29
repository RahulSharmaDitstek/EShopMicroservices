<<<<<<< HEAD
﻿using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.API.DependencyInjection;
=======
﻿namespace Ordering.API.DependencyInjection;
>>>>>>> c5bc8b4a6334653d1e8a8cf72b2406d021f9e706
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

<<<<<<< HEAD
        app.UseHealthChecks("/health",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
=======
        app.UseHealthChecks("/health");
>>>>>>> c5bc8b4a6334653d1e8a8cf72b2406d021f9e706
        return app;
    }
}
