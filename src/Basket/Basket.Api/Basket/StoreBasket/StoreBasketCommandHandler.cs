using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.CQRS;
using Discount.Grpc;
using FluentValidation;

namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(z => z.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(z => z.Cart.UserName).NotEmpty().WithMessage("UserName can not be null");
    }
}

public class StoreBasketCommandHandler(ILogger<StoreBasketCommandHandler> logger, IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountService) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Storing basket with username {username}", query.Cart.UserName);

        foreach (var product in query.Cart.CartItems)
        {
            var coupon = await discountService.GetDiscountAsync(new GetDiscountRequest{ProductName = product.ProductName}, cancellationToken: cancellationToken);

            product.Price -= coupon.Amount;
        }
        
        await basketRepository.StoreBasketAsync(query.Cart, cancellationToken);
        
        return new StoreBasketResult(query.Cart.UserName);
    }
}