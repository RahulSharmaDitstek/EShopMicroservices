//using System.Reflection;
//using BuildingBlocks.Behaviour;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace Catalog.API.DependencyInjection;
//public static class DIConfigurations
//{
//    public static IServiceCollection AddBuildingBlocksServices(this IServiceCollection services, ConfigurationManager configuration)
//    {
//        services.AddMediatR(cfg =>
//        {
//            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
//        });

//        return services;
//    }
//}

