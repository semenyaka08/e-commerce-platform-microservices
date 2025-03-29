using System.Text.Json;
using Basket.Api.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Api.Data;

public class BasketCashingRepository(IBasketRepository basketRepository, IDistributedCache distributedCache) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken)
    {
        var cashedBasket = await distributedCache.GetStringAsync(userName, token: cancellationToken);

        if (!string.IsNullOrEmpty(cashedBasket))
            return JsonSerializer.Deserialize<ShoppingCart>(cashedBasket)!;

        var basket = await basketRepository.GetBasketAsync(userName, cancellationToken);

        await distributedCache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken)
    {
        await basketRepository.StoreBasketAsync(cart, cancellationToken);

        await distributedCache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart), cancellationToken);

        return cart;
    }

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken)
    {
        await basketRepository.DeleteBasketAsync(userName, cancellationToken);

        await distributedCache.RemoveAsync(userName, cancellationToken);

        return true;
    }
}