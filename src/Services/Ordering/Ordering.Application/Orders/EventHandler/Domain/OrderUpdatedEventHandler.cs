namespace Ordering.Application.Orders.EventHandler.Domain;

public class OrderUpdatedEventHandler(ILogger<OrderUpdatedDomainEvent> logger) : INotificationHandler<OrderUpdatedDomainEvent>
{
    public Task Handle(OrderUpdatedDomainEvent notification, CancellationToken cancellationToken)
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

