namespace Ordering.API.Endpoints;

<<<<<<< HEAD
=======
public record GetOrdersRequest();
>>>>>>> c5bc8b4a6334653d1e8a8cf72b2406d021f9e706
public record GetOrdersResponse(PaginatedResult<OrderDto> Result);
public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderQuery(request));

<<<<<<< HEAD
            var dtoPage = result.Orders.Adapt<PaginatedResult<OrderDto>>();
            var response = new GetOrdersResponse(dtoPage);
=======
            var response = result.Adapt<GetOrdersByCustomerResponse>();
>>>>>>> c5bc8b4a6334653d1e8a8cf72b2406d021f9e706

            return Results.Ok(response);
        })
         .WithName("GetOrders")
<<<<<<< HEAD
         .Produces<GetOrdersResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .ProducesProblem(StatusCodes.Status404NotFound)
         .WithSummary("Get all Orders")
         .WithDescription("This endpoint allows you to get all the orders");
=======
         .Produces<GetOrdersByNameResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .ProducesProblem(StatusCodes.Status404NotFound)
         .WithSummary("Get all Orders by Customer")
         .WithDescription("This endpoint allows you to get all the order by their Customer");
>>>>>>> c5bc8b4a6334653d1e8a8cf72b2406d021f9e706
    }
}