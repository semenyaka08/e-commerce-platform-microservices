using BuildingBlocks.RabbitMQ.Events;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Enums;

namespace Ordering.Application.Extensions;

public static class BrokerMessageMapper
{
    public static CreateOrderCommand ToCommand(this BasketCheckoutEvent checkoutEvent)
    {
        var billingAddress = new AddressDto(checkoutEvent.FirstName, checkoutEvent.LastName, checkoutEvent.EmailAddress,
            checkoutEvent.AddressLine, checkoutEvent.Country, checkoutEvent.State, checkoutEvent.ZipCode);
        var shippingAddress = new AddressDto(checkoutEvent.FirstName, checkoutEvent.LastName, checkoutEvent.EmailAddress,
            checkoutEvent.AddressLine, checkoutEvent.Country, checkoutEvent.State, checkoutEvent.ZipCode);
        var paymentAddress = new PaymentDto(checkoutEvent.CardName, checkoutEvent.CardNumber, checkoutEvent.Expiration, checkoutEvent.Cvv, checkoutEvent.PaymentMethod);

        var orderId = Guid.NewGuid();

        var mappedOrderItems = checkoutEvent.Items.Select(z => z.ToDto(orderId)).ToList();
        
        var orderDto = new OrderDto(orderId, checkoutEvent.CustomerId, checkoutEvent.UserName, shippingAddress, billingAddress, paymentAddress, OrderStatus.Pending, mappedOrderItems);

        return new CreateOrderCommand(orderDto);
    }

    private static OrderItemDto ToDto(this CartItem item, Guid orderId)
    {
        return new OrderItemDto(orderId, item.ProductId, item.Quantity, item.Price);
    }
}