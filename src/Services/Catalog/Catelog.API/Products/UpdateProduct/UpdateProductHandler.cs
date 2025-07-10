namespace Catalog.API.Products.UpdateProduct;

public class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdatedProductResult>
{
    public async Task<UpdatedProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        try
        {
            //create Product entity from command object
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken) ?? throw new ProductNotFoundException(command.Id);

            product.Name = command.Name;
            product.Category = command.Category;
            product.Price = command.Price;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
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
