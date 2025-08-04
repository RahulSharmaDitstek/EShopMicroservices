namespace Shopping.Web.Models.Ordering;

public record OrderModel(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressModel ShippingAddress,
    AddressModel BillingAddress,
    PaymentModel Payment,
    OrderStatus Status,
    List<OrderItemModel> OrderItems);
public record AddressModel(string FirstName,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string Country,
    string State,
    string ZipCode);
public record PaymentModel(string CardName,
    string CardNumber,
    string Expiration,
    string Cvv,
    int PaymentMethod);

public enum OrderStatus
{
    Pending,
    Shipped,
    Draft,
    Delivered,
    Cancelled,
    Returned,
    Refunded,
    Completed,
    Processing,
    OnHold,
    Failed,
    AwaitingPayment,
    AwaitingShipment,
    PartiallyShipped,
    PartiallyDelivered,
    AwaitingFulfillment,
    AwaitingPickup,
    AwaitingConfirmation,
    AwaitingReview,
    AwaitingRestock,
    AwaitingApproval,
    AwaitingAction,
    AwaitingResponse,
    AwaitingCustomerAction,
    AwaitingSupplierAction,
}
public record OrderItemModel(Guid OrderId,
    Guid ProductId,
    int Quantity,
    decimal Price);

public record GetOrderResponse(IEnumerable<PaginatedResult<OrderModel>> Orders);
public record GetOrdersByNameResponse(IEnumerable<OrderModel> Orders);
public record GetOrdersByCustomerResponse(IEnumerable<OrderModel> Orders);
