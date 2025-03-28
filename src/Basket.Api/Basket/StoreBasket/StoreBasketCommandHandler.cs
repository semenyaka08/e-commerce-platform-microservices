using Basket.Api.Models;
using BuildingBlocks.CQRS;
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

public class StoreBasketCommandHandler(ILogger<StoreBasketCommandHandler> logger) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public Task<StoreBasketResult> Handle(StoreBasketCommand query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Storing basket with username {username}", query.Cart.UserName);

        throw new NotImplementedException();
    }
}