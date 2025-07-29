namespace Ordering.API.Endpoints;


public record GetOrdersResponse(PaginatedResult<OrderDto> Result);
public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderQuery(request));

            var dtoPage = result.Orders.Adapt<PaginatedResult<OrderDto>>();
            var response = new GetOrdersResponse(dtoPage);

            return Results.Ok(response);
        })
         .WithName("GetOrders")
         .Produces<GetOrdersResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .ProducesProblem(StatusCodes.Status404NotFound)
         .WithSummary("Get all Orders")
         .WithDescription("This endpoint allows you to get all the orders");
    }
}