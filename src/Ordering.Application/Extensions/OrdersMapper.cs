using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Extensions;

public static class OrdersMapper
{
    public static OrderDto ToDto(this Order order)
    {
        var mappedOrderItems = order.OrderItems.Select(z => z.ToDto()).ToList();

        return new OrderDto(order.Id.Value, order.CustomerId.Value, order.OrderName.Value,
            order.ShippingAddress.ToDto(), order.BillingAddress.ToDto(), order.Payment.ToDto(), order.Status,
            mappedOrderItems);
    }

    private static AddressDto ToDto(this Address address)
    {
        return new AddressDto(address.FirstName, address.LastName, address.EmailAddress!,
            address.AddressLine, address.Country, address.State, address.ZipCode);
    }

    private static PaymentDto ToDto(this Payment payment)
    {
        return new PaymentDto(payment.CardName!, payment.CardNumber, payment.Expiration, payment.Cvv,
            payment.PaymentMethod);
    }

    private static OrderItemDto ToDto(this OrderItem orderItem)
    {
        return new OrderItemDto(orderItem.OrderId.Value, orderItem.ProductId.Value, orderItem.Quantity,
            orderItem.Price);
    }
}