namespace Catalog.API.Products.GetProductByCategory;

public class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResponse>
{

    public async Task<GetProductByCategoryResponse> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdCategory.Handle called with {@Query}", query);

        var products = await session.Query<Product>()
                    .Where(p => p.Category.Contains(query.Category))
                    .ToListAsync(token: cancellationToken);
        return products.Count > 0
           ? await Task.FromResult(new GetProductByCategoryResponse(products))
           : await Task.FromResult(new GetProductByCategoryResponse([]));
    }
}
