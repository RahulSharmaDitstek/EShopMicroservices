namespace Shopping.Web.DependencyInjection;
public static class DIConfigurations
{
    public static IServiceCollection AddWebAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        var gatewayAddress = configuration.GetRequiredSection("ApiSettings:GatewayAddress").Value ?? throw new InvalidOperationException("Gateway Address not configured in App.Settings.");

        services.AddRefitClient<ICatalogService>()
                .ConfigureHttpClient(c => { c.BaseAddress = new Uri(gatewayAddress); });
        services.AddRefitClient<IBasketService>()
               .ConfigureHttpClient(c => { c.BaseAddress = new Uri(gatewayAddress); });
        services.AddRefitClient<IOrderingService>()
               .ConfigureHttpClient(c => { c.BaseAddress = new Uri(gatewayAddress); });


        AppConstants.GatewayAddress = gatewayAddress ?? string.Empty;



        //services.AddSingleton(sp =>
        //    sp.GetRequiredService<IOptions<ApiSettings>>().Value);

        return services;
    }
}
