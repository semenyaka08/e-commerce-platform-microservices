using Basket.Api.Models;
using BuildingBlocks.CQRS;

namespace Basket.Api.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart ShoppingCart);

public class GetBasketQueryHandler(ILogger<GetBasketQueryHandler> logger) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching basket with username {username}", query.UserName);

        throw new NotImplementedException();
    }
}