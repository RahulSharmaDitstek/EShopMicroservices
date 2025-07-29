namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext _dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (command.Order == null)
                Results.BadRequest(new { Field = nameof(command.Order), Message = "Order cannot be null." });

            var order = CreateNewOrder(command.Order);

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id.Value);
        }
        catch (Exception)
        {

            throw;
        }
    }

    private static Order CreateNewOrder(OrderDto? orderDto)
    {
        var shippingAddress = Address.Of(orderDto!.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);

        var billingAddress = Address.Of(orderDto!.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);

        var payment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

        var newOrder = Order.CreateOrderItem(
            OrderId.Of(Guid.NewGuid()),
            CustomerId.Of(orderDto.CustomerId),
            OrderName.Of(orderDto.OrderName),
            shippingAddress,
            billingAddress,
            payment);

        foreach (var item in orderDto.OrderItems)
        {
            newOrder.AddOrderItem(ProductId.Of(item.ProductId), item.Quantity, item.Price);
        }
        return newOrder;
    }
}
