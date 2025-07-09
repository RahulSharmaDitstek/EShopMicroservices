namespace Catalog.API.Products.GetProductById;
public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetProductByIdHandler.Handle called with {@Query}", query);

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken) ?? throw new NullReferenceException($"Product with ID {query.Id} not found.");
        return new GetProductByIdResult(product);
    }
}

