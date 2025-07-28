namespace Ordering.API.Endpoints;

public record GetOrdersRequest();
public record GetOrdersResponse(PaginatedResult<OrderDto> Result);
public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderQuery(request));

            var response = result.Adapt<GetOrdersByCustomerResponse>();

            return Results.Ok(response);
        })
         .WithName("GetOrders")
         .Produces<GetOrdersByNameResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .ProducesProblem(StatusCodes.Status404NotFound)
         .WithSummary("Get all Orders by Customer")
         .WithDescription("This endpoint allows you to get all the order by their Customer");
    }
}