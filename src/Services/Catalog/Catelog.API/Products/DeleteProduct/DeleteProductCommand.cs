namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IQuery<DeleteProductResult>;
