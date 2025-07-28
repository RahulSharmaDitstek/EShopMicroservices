namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersResult(PaginatedResult<OrderDto> Orders);