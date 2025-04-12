using Basket.Api.Dtos;
using Carter;
using MediatR;

namespace Basket.Api.Basket.CheckoutBasket;

public record CheckoutBasketRequest(CheckoutBasketDto CheckoutBasketDto);

public record CheckoutBasketResponse(bool IsSucceeded);

public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
        {
            var command = new CheckoutBasketCommand(request.CheckoutBasketDto);
            
            var result = await sender.Send(command);

            return Results.Ok(new CheckoutBasketResponse(result.IsSucceeded));
        })
        .WithName("CheckoutBasket")
        .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout Cart")
        .WithDescription("Checkout Product");;
    }
}