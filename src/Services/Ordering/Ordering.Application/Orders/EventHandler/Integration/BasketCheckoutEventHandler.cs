namespace Ordering.Application.Orders.EventHandler.Integration;

public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        try
        {
            logger.LogInformation("Integration Event Handled: {IntegrationEvent}", context.Message);
            var message = context.Message;
            var command = MapToCreateOrderCommand(message);

            await sender.Send(command);
            logger.LogInformation("Order created successfully for BasketCheckoutEvent with OrderId: {OrderId}", command.Order.Id);

        }
        catch (Exception)
        {
            throw;
        }

    }
    private static CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        var orderId = Guid.NewGuid();

        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);

        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.Cvv, message.PaymentMethod);

        var orderDto = new OrderDto
        (
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.UserName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            Status: OrderStatus.Pending,
            OrderItems:
            [
                new OrderItemDto(OrderId: orderId, ProductId: new Guid("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"), Quantity: 2, Price: 500),
                new OrderItemDto(OrderId: orderId, ProductId: new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), Quantity: 1, Price: 400),
            ]
        );
        return new CreateOrderCommand(orderDto);

    }
}

