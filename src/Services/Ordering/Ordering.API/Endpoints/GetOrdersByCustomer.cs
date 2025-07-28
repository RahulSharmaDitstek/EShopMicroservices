namespace Ordering.API.Endpoints;
public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);
public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderByCustomerQuery(customerId));

            var response = result.Adapt<GetOrdersByCustomerResponse>();

            return Results.Ok(response);
        })
         .WithName("GetOrdersByCustomer")
         .Produces<GetOrdersByNameResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .ProducesProblem(StatusCodes.Status404NotFound)
         .WithSummary("Get all Orders by Customer")
         .WithDescription("This endpoint allows you to get all the order by their Customer");
    }
}
