namespace BuildingBlocks.Messaging.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
    {
        var host = configuration.GetRequiredSection("MessageBroker:HostName").Value ?? throw new InvalidOperationException("RabbitMQ host name not configured.");
        var userName = configuration.GetRequiredSection("MessageBroker:UserName").Value ?? throw new InvalidOperationException("RabbitMQ User Name not configured.");
        var password = configuration.GetRequiredSection("MessageBroker:Password").Value ?? throw new InvalidOperationException("RabbitMQ Password not configured.");

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            if (assembly != null)
            {
                x.AddConsumers(assembly);
            }
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(host), h =>
                {
                    h.Username(userName);
                    h.Password(password);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}
