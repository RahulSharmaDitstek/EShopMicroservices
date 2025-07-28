namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrderByCustomerResult(IEnumerable<OrderDto>Orders );
