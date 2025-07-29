namespace Ordering.Application.Orders.EventHandler.Domain;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedDomainEvent>
{
    public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Domain Event Handled :{DomainEvent}", notification.GetType());
            return Task.CompletedTask;

        }
        catch (Exception)
        {

            throw;
        }
    }
}

