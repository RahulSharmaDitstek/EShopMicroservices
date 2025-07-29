namespace Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrderByNameHandler(IApplicationDbContext _dbContext) : IQueryHandler<GetOrderByNameQuery, GetOrderByNameResult>
{
    public async Task<GetOrderByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
    {
        try
        {
<<<<<<< HEAD

            var orders = await _dbContext.Orders
                        .Include(o => o.OrderItems)
                        .Where(o => o.OrderName.Value.Contains(query.Name))
                        .OrderBy(o => o.OrderName.Value)
                        .ToListAsync(cancellationToken);

            return new GetOrderByNameResult(orders.ToOrderDtoList());
=======
            var orders = await _dbContext.Orders
                        .Include(o => o.OrderItems)
                        .Where(o => o.OrderName.Value.Contains(query.Name))
                        .OrderBy(o => o.OrderName)
                        .ToListAsync(cancellationToken);

            return new GetOrderByNameResult((IEnumerable<Order>)orders.ToOrderDtoList());
>>>>>>> c5bc8b4a6334653d1e8a8cf72b2406d021f9e706

        }
        catch (Exception)
        {

            throw;
        }
    }
  
}
