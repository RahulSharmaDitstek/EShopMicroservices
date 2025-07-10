namespace Catalog.API.Products.GetProductById;
public class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken = default)
    {
        try
        {

            var product = await session.LoadAsync<Product>(query.Id, cancellationToken) ?? throw new ProductNotFoundException(query.Id);
            return new GetProductByIdResult(product);
        }
        catch (Exception)
        {

            throw;
        }
    }
}

