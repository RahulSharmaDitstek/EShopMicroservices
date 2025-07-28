namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;
public class GetOrderByCustomerHandler(ILogger<GetOrderByCustomerHandler> logger, IApplicationDbContext _dbContext) : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerResult>
{
    public async Task<GetOrderByCustomerResult> Handle(GetOrderByCustomerQuery query, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation($"Fetch the Get Order By Customer");
            var orders = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);

            return new GetOrderByCustomerResult(orders.ToOrderDtoList());
        }
        catch (Exception)
        {

            throw;
        }
    }
}
