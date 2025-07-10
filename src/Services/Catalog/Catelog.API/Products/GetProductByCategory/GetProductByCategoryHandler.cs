namespace Catalog.API.Products.GetProductByCategory;

public class GetProductByCategoryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResponse>
{

    public async Task<GetProductByCategoryResponse> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {

        try
        {
            var products = await session.Query<Product>()
                    .Where(p => p.Category.Contains(query.Category))
                    .ToListAsync(token: cancellationToken);
            return products.Count > 0
               ? await Task.FromResult(new GetProductByCategoryResponse(products))
               : await Task.FromResult(new GetProductByCategoryResponse([]));

        }
        catch (Exception)
        {

            throw;
        }
    }
}
