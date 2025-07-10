using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;
public class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken = default)
    {
        try
        {
            var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);
            return products.Count > 0
                ? await Task.FromResult(new GetProductsResult(products))
                : await Task.FromResult(new GetProductsResult([]));
        }
        catch (Exception)
        {

            throw;
        }
    }
}

