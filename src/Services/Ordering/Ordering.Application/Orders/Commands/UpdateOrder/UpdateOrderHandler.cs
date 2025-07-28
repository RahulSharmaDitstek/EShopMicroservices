namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext _dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var orderId = OrderId.Of(command.Order.Id);
            var order = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken) ?? throw new OrderNotFoundException(command.Order.Id);

            CreateUpdatedOrder(order, command.Order);

            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static Order CreateUpdatedOrder(Order existingOrder, OrderDto? orderDto)
    {
        var shippingAddress = Address.Of(orderDto!.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);

        var billingAddress = Address.Of(orderDto!.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);

        var payment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

        existingOrder.UpdateOrderItem(
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: shippingAddress,
            billingAddress: billingAddress,
            payment: payment,
            status: orderDto.Status);

        foreach (var item in orderDto.OrderItems)
        {
            existingOrder.AddOrderItem(ProductId.Of(item.ProductId), item.Quantity, item.Price);
        }
        return existingOrder;
    }
}
