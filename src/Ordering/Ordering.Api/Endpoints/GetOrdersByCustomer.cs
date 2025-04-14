using Carter;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.Api.Endpoints;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var query = new GetOrdersByCustomerQuery(customerId);

            var result = await sender.Send(query);

            var response = new GetOrdersByCustomerResponse(result.Orders);

            return Results.Ok(response);
        }).WithName("GetOrdersByCustomer")
        .Produces<GetOrdersByCustomerResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Orders by customer")
        .WithDescription("Get Orders by customer");
    }
}