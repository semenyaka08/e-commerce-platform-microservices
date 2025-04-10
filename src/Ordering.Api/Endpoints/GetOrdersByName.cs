using Carter;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.Api.Endpoints;

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
        {
            var query = new GetOrdersByNameQuery(orderName);

            var result = await sender.Send(query);

            var response = new GetOrdersByNameResponse(result.Orders);
            
            return Results.Ok(response);
        }).WithName("GetOrdersByName")
        .Produces<GetOrdersByNameResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Orders by name")
        .WithDescription("Get Orders by name");;
    }
}