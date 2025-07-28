namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrderQuery (PaginationRequest paginationRequest):IQuery<GetOrdersResult>;

