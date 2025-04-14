using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Extensions;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger, IPublishEndpoint publishEndpoint) : INotificationHandler<OrderUpdatedEvent>
{
    public async Task Handle(OrderUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var orderUpdatedEvent = domainEvent.Order.ToUpdateEvent();

        await publishEndpoint.Publish(orderUpdatedEvent, cancellationToken);
    }
}