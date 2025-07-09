namespace Catalog.API.Products.UpdateProduct;

public class UpdateProductCommandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            var response = result.Adapt<UpdatedProductResult>();

            return Results.Ok(response);
        })
         .WithName("UpdateProduct")
         .Produces<UpdatedProductResult>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Update Product")
         .WithDescription("Update Product");

    }
}
