using BuildingBlocks.Pagination;
using Carter;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.Api.Endpoints;

public record GetOrdersResponse(PaginationResult<OrderDto> PaginationResult);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var query = new GetOrdersQuery(request);

            var result = await sender.Send(query);

            var response = new GetOrdersResponse(result.PaginationResult);

            return Results.Ok(response);
        }).WithName("GetOrders")
        .Produces<GetOrders>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Orders")
        .WithDescription("Get Orders");;
    }
}