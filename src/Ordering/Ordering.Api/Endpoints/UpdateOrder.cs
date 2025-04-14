using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.Api.Endpoints;

public record UpdateOrderRequest(OrderDto OrderDto);

public record UpdateOrderResponse(bool IsSucceeded);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async ([FromBody] UpdateOrderRequest request, ISender sender) =>
        {
            var command = new UpdateOrderCommand(request.OrderDto);

            var result = await sender.Send(command);

            var response = new UpdateOrderResponse(result.IsSucceeded);

            return Results.Ok(response);
        }).WithName("UpdateOrder")
        .Produces<UpdateOrderResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Order")
        .WithDescription("Update Order");
    }
}