namespace Ordering.Application.Orders.EventHandler.Domain;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger, IPublishEndpoint publishEndpoint, IFeatureManager featureManager) : INotificationHandler<OrderCreatedDomainEvent>
{
    public async Task Handle(OrderCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Domain Event Handled :{DomainEvent}", domainEvent.GetType());

            if (await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();

                // Publish the event to the message broker
                await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            }



        }
        catch (Exception)
        {

            throw;
        }
    }
}