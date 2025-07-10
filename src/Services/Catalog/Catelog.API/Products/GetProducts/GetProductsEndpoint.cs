namespace Catalog.API.Products.GetProducts;
public record GetProductRequest(int? PageNumber = 1, int? PageSize = 10);
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetProductsQuery>();
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductsResult>();

            return Results.Ok(response);
        })
         .WithName("GetProducts")
         .Produces<GetProductsResult>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Products")
         .WithDescription("Get Products");

    }
}
