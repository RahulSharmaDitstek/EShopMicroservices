namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext _dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var orderId = OrderId.Of(command.OrderId);
            var order = await _dbContext.Orders.FindAsync([command.OrderId], cancellationToken: cancellationToken) ?? throw new OrderNotFoundException(command.OrderId);

            if (order.Status != OrderStatus.Pending)
            {
                throw new InvalidOperationException("Only pending orders can be deleted.");
            }

            _dbContext.Orders.Remove(order);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteOrderResult(true);

        }
        catch (Exception)
        {

            throw;
        }
    }
}