using Basket.Api.Models;
using Carter;
using MediatR;

namespace Basket.Api.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart ShoppingCart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName));

            var response = new GetBasketResponse(result.ShoppingCart);

            return Results.Ok(response);
            
        }).WithName("GetBasketByUserName")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Basket By UserName")
        .WithDescription("Get Basket By UserName");;
    }
}