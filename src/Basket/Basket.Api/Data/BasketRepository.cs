using Basket.Api.Exceptions;
using Basket.Api.Models;
using Marten;

namespace Basket.Api.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);

        if (basket == null)
            throw new BasketNotFoundException(nameof(ShoppingCart), userName);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        session.Store(cart);

        await session.SaveChangesAsync(cancellationToken);

        return cart;
    }

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var cart = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
        
        if (cart == null)
            throw new BasketNotFoundException(nameof(ShoppingCart), userName);
        
        session.Delete<ShoppingCart>(userName);

        await session.SaveChangesAsync(cancellationToken);

        return true;
    }
}