namespace Catalog.API.Products.CreateProduct;
public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();

            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{response.Id}", response);
        })
         .WithName("CreateProduct")
         .Produces<CreateProductResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Create a new product")
         .WithDescription("This endpoint allows you to create a new product in the catalog. " +
                             "You need to provide the product details such as name, category, description, image file, and price.");
    }
}
