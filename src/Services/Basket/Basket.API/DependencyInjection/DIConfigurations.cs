using BuildingBlocks.Exceptions.Handler;

namespace Basket.API.DependencyInjection;
public static class DIConfigurations
{
    public static IServiceCollection AddBasketServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        //Application ervices
        var assembly = typeof(Program).Assembly;
        var dbConnString = configuration.GetConnectionString("DatabaseConnection") ?? throw new InvalidOperationException("Database connection string not configured.");
        var redisConnString = configuration.GetConnectionString("Redis") ?? throw new InvalidOperationException("Redis connection string not configured.");

        services.AddCarter();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
        });
        //services.AddValidatorsFromAssembly(assembly);
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.Decorate<IBasketRepository, CachedBasketRepository>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnString;
        });

        //Use Scrutor library to handle this manually
        //services.AddScoped<IBasketRepository>(provider =>
        //{
        //    var basketRepository = provider.GetService<IBasketRepository>();
        //    return new CachedBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());
        //});

        //Data Services
        services.AddMarten(options =>
         {
             options.Connection(dbConnString);
             options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
         }).UseLightweightSessions();


        //Grpc Services
        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
        {
            options.Address = new Uri(configuration["GrpcSettings:DiscountUrl"] ?? throw new InvalidOperationException("Discount gRPC URL not configured."));
        })
         .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                return handler;
            });

        //Exception Handler
        services.AddExceptionHandler<CustomExceptionHandler>();

        //Health Checks
        services.AddHealthChecks()
            .AddNpgSql(dbConnString)
            .AddRedis(redisConnString);
        return services;
    }
}
