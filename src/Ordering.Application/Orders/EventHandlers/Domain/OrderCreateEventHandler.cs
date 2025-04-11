using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Extensions;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreateEventHandler(ILogger<OrderCreateEventHandler> logger, IPublishEndpoint publishEndpoint) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var orderCreatedEvent = domainEvent.Order.ToDto();

        await publishEndpoint.Publish(orderCreatedEvent, cancellationToken);
    }
}