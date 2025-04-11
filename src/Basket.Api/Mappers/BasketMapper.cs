using Basket.Api.Dtos;
using Basket.Api.Models;
using BuildingBlocks.RabbitMQ.Events;

namespace Basket.Api.Mappers;

public static class BasketMapper
{
    public static BasketCheckoutEvent ToEvent(this CheckoutBasketDto basketDto)
    {
        return new BasketCheckoutEvent
        {
            UserName = basketDto.UserName,
            CustomerId = basketDto.CustomerId,
            FirstName = basketDto.FirstName,
            LastName = basketDto.LastName,
            EmailAddress = basketDto.EmailAddress,
            AddressLine = basketDto.AddressLine,
            Country = basketDto.Country,
            State = basketDto.State,
            ZipCode = basketDto.ZipCode,
            CardName = basketDto.CardName,
            CardNumber = basketDto.CardNumber,
            Expiration = basketDto.Expiration,
            Cvv = basketDto.Cvv,
            PaymentMethod = basketDto.PaymentMethod
        };
    }

    public static CartItem ToEventItem(this ShoppingCartItem item)
    {
        return new CartItem
        {
            ProductName = item.ProductName,
            Quantity = item.Quantity,
            ProductId = item.ProductId,
            Price = item.Price
        };
    }
}