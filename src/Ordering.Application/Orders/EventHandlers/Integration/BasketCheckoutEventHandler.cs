using BuildingBlocks.RabbitMQ.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ILogger<BasketCheckoutEventHandler> logger, ISender sender) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("Handling basket checkout event with id: {Id}", context.Message.Id);

        var command = context.Message.ToCommand();

        await sender.Send(command);
    }
}