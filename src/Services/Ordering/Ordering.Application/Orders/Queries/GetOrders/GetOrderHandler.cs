namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrderHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrderQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrderQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .OrderBy(o => o.OrderName.Value)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetOrdersResult(new PaginatedResult<OrderDto>(
                pageIndex,
                pageSize,
                totalCount,
               orders.ToOrderDtoList()));

        }
        catch (Exception)
        {

            throw;
        }
    }
}
