
namespace Catalog.API.Products.GetProducts;
public class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetProductsQueryHandler.Handler called with {@Query}", query);
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return products.Count > 0
            ? await Task.FromResult(new GetProductsResult(products))
            : await Task.FromResult(new GetProductsResult([]));
    }
}

