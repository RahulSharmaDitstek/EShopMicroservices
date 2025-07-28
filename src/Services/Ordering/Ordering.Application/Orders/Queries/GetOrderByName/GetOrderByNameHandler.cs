namespace Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrderByNameHandler(IApplicationDbContext _dbContext) : IQueryHandler<GetOrderByNameQuery, GetOrderByNameResult>
{
    public async Task<GetOrderByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
    {
        try
        {

            var orders = await _dbContext.Orders
                        .Include(o => o.OrderItems)
                        .Where(o => o.OrderName.Value.Contains(query.Name))
                        .OrderBy(o => o.OrderName.Value)
                        .ToListAsync(cancellationToken);

            return new GetOrderByNameResult(orders.ToOrderDtoList());

        }
        catch (Exception)
        {

            throw;
        }
    }
  
}
