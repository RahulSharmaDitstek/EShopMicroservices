namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;
public class GetOrderByCustomerHandler(ILogger<GetOrderByCustomerHandler> logger, IApplicationDbContext _dbContext) : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerResult>
{
    public async Task<GetOrderByCustomerResult> Handle(GetOrderByCustomerQuery query, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Fetching orders for customer with ID: {CustomerId}", query.CustomerId);
            var customerId = CustomerId.Of(query.CustomerId);

            var orders = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.CustomerId == customerId)
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);

            if (orders.Count == 0)
            {
                logger.LogInformation("No orders found for customer ID: {CustomerId}", query.CustomerId);
                //Results.NotFound($"No orders found for customer ID: {query.CustomerId} ");
                throw new NotFoundException($"No orders found for customer {query.CustomerId}");

            }
            else
            {
                logger.LogInformation("Retrieved {OrderCount} orders for customer {CustomerId}", orders.Count, query.CustomerId);
            }

            var dtoList = orders.ToOrderDtoList();

            return new GetOrderByCustomerResult(dtoList);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
