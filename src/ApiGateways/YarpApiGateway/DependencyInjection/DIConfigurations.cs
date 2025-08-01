namespace YarpApiGateway.DependencyInjection;
public static class DIConfigurations
{
    public static IServiceCollection AddYearpApiGatewayServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var reverseProxy = configuration.GetRequiredSection("ReverseProxy") ?? throw new InvalidOperationException("ReverseProxy not configured.");

        services.AddReverseProxy().LoadFromConfig(reverseProxy);

        services.AddRateLimiter(rateLimiterOptions =>
        {
            rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
            {
                options.Window = TimeSpan.FromSeconds(10);
                options.PermitLimit = 5;
                options.QueueLimit = 5;
            });
        });
        return services;
    }
}
