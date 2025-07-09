namespace Catalog.API.Products.UpdateProduct;

public class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdatedProductResult>
{
    public async Task<UpdatedProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductHandler.Handle called with {@command}", command);

        try
        {
            //create Product entity from command object
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken) ?? throw new NullReferenceException($"Product with ID {command.Id} not found.");
            product.Name = command.Name;
            product.Category = command.Category;
            product.Price = command.Price;
            product.Description = command.Description;
            product.ImageFile= command.ImageFile;
            //save a database
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            //return CreateProductResult result
            return new UpdatedProductResult(product.Id);

        }
        catch (Exception)
        {

            throw;
        }
    }
}
