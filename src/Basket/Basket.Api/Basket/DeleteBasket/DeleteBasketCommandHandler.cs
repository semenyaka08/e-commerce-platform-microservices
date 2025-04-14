using Basket.Api.Data;
using BuildingBlocks.CQRS;

namespace Basket.Api.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSucceeded);

public class DeleteBasketCommandHandler(ILogger<DeleteBasketCommandHandler> logger, IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting cart with username: {userName}", command.UserName);

        await basketRepository.DeleteBasketAsync(command.UserName, cancellationToken);

        return new DeleteBasketResult(true);
    }
}