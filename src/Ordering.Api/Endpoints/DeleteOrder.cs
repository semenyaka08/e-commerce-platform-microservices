using Carter;
using MediatR;
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.Api.Endpoints;

public record DeleteOrderResponse(bool IsSucceeded);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{orderId}", async (Guid orderId, ISender sender) =>
        {
            var command = new DeleteOrderCommand(orderId);

            var result = await sender.Send(command);

            var response = new DeleteOrderResponse(result.IsSucceeded);
            
            return Results.Ok(response);
        }).WithName("DeleteOrder")
        .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Order")
        .WithDescription("Delete Order");;
    }
}