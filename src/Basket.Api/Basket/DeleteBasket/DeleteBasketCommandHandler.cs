using BuildingBlocks.CQRS;

namespace Basket.Api.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSucceeded);

public class DeleteBasketCommandHandler(ILogger<DeleteBasketCommandHandler> logger) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting cart with username: {userName}", command.UserName);

        throw new NotImplementedException();
    }
}