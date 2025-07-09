namespace Catalog.API.Products.DeleteProduct;

public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : IQueryHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
		try
		{
            logger.LogInformation("DeleteProductResult.Handle called with {@command}", command);

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken) ?? throw new NullReferenceException($"Product with ID {command.Id} not found.");
            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
		catch (Exception)
		{

			throw;
		}
    }
}
