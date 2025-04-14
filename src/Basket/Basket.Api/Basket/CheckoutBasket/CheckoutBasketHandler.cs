using Basket.Api.Data;
using Basket.Api.Dtos;
using Basket.Api.Mappers;
using BuildingBlocks.CQRS;
using MassTransit;

namespace Basket.Api.Basket.CheckoutBasket;

public record CheckoutBasketCommand(CheckoutBasketDto CheckoutBasketDto) : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSucceeded);

public class CheckoutBasketHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasketAsync(request.CheckoutBasketDto.UserName, cancellationToken);

        if (basket == null)
            return new CheckoutBasketResult(false);

        var eventMessage = request.CheckoutBasketDto.ToEvent();

        eventMessage.TotalPrice = basket.TotalPrice;
        eventMessage.Items = basket.CartItems.Select(z=>z.ToEventItem()).ToList();
        
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await basketRepository.DeleteBasketAsync(request.CheckoutBasketDto.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}