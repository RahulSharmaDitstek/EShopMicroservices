namespace Ordering.Application.Orders.Queries.GetOrders;
public record GetOrderQuery (PaginationRequest PaginationRequest):IQuery<GetOrdersResult>;
