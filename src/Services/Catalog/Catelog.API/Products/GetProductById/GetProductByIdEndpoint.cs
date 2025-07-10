namespace Catalog.API.Products.GetProductById;

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id,ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductByIdResult>();

            return Results.Ok(response);
        })
         .WithName("GetProductById")
         .Produces<GetProductByIdResult>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Products")
         .WithDescription("Get Products");

    }
}