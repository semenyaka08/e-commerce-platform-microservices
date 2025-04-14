using Basket.Api.Models;
using Carter;
using MediatR;

namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);

public record StoreBasketResponse(string UserName);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {
            var result = await sender.Send(new StoreBasketCommand(request.Cart));

            var response = new StoreBasketResponse(result.UserName);

            return Results.Created($"basket/{response.UserName}", response);
        })
        .WithName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store Cart")
        .WithDescription("Store Product");
    }
}