using BuildingBlocks.RabbitMQ.Events;
using BuildingBlocks.RabbitMQ.Events.Models;
using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Extensions;

public static class OrdersMapper
{
    public static OrderCreateEvent ToCreateEvent(this Order order)
    {
        var mappedOrderItems = order.OrderItems.Select(z => z.ToEventOrderItem()).ToList();

        return new OrderCreateEvent
        {
            OrderId = order.Id.Value,
            CustomerId = order.CustomerId.Value,
            OrderName = order.OrderName.Value,
            FirstNameShippingAddress = order.ShippingAddress.FirstName,
            LastNameShippingAddress = order.ShippingAddress.LastName,
            EmailAddressShippingAddress = order.ShippingAddress.EmailAddress,
            AddressLineShippingAddress = order.ShippingAddress.AddressLine,
            CountryShippingAddress = order.ShippingAddress.Country,
            StateShippingAddress = order.ShippingAddress.State,
            ZipCodeShippingAddress = order.ShippingAddress.ZipCode,
            FirstNameBillingAddress = order.BillingAddress.FirstName,
            LastNameBillingAddress = order.BillingAddress.LastName,
            EmailAddressBillingAddress = order.BillingAddress.EmailAddress,
            AddressLineBillingAddress = order.BillingAddress.AddressLine,
            CountryBillingAddress = order.BillingAddress.Country,
            StateBillingAddress = order.BillingAddress.State,
            ZipCodeBillingAddress = order.BillingAddress.ZipCode,
            CardName = order.Payment.CardName,
            CardNumber = order.Payment.CardNumber,
            Expiration = order.Payment.Expiration,
            Cvv = order.Payment.Cvv,
            PaymentMethod = order.Payment.PaymentMethod,
            OrderStatus = order.Status.ToString(),
            OrderItems = mappedOrderItems
        };
    }

    public static OrderUpdateEvent ToUpdateEvent(this Order order)
    {
        var mappedOrderItems = order.OrderItems.Select(z => z.ToEventOrderItem()).ToList();

        return new OrderUpdateEvent
        {
            OrderId = order.Id.Value,
            CustomerId = order.CustomerId.Value,
            OrderName = order.OrderName.Value,
            FirstNameShippingAddress = order.ShippingAddress.FirstName,
            LastNameShippingAddress = order.ShippingAddress.LastName,
            EmailAddressShippingAddress = order.ShippingAddress.EmailAddress,
            AddressLineShippingAddress = order.ShippingAddress.AddressLine,
            CountryShippingAddress = order.ShippingAddress.Country,
            StateShippingAddress = order.ShippingAddress.State,
            ZipCodeShippingAddress = order.ShippingAddress.ZipCode,
            FirstNameBillingAddress = order.BillingAddress.FirstName,
            LastNameBillingAddress = order.BillingAddress.LastName,
            EmailAddressBillingAddress = order.BillingAddress.EmailAddress,
            AddressLineBillingAddress = order.BillingAddress.AddressLine,
            CountryBillingAddress = order.BillingAddress.Country,
            StateBillingAddress = order.BillingAddress.State,
            ZipCodeBillingAddress = order.BillingAddress.ZipCode,
            CardName = order.Payment.CardName,
            CardNumber = order.Payment.CardNumber,
            Expiration = order.Payment.Expiration,
            Cvv = order.Payment.Cvv,
            PaymentMethod = order.Payment.PaymentMethod,
            OrderStatus = order.Status.ToString(),
            OrderItems = mappedOrderItems
        };
    }
    
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
        return new OrderItemDto(orderItem.OrderId.Value, orderItem.Quantity,
            orderItem.Price);
    }
    
    private static EventOrderItem ToEventOrderItem(this OrderItem orderItem)
    {
        return new EventOrderItem(orderItem.OrderId.Value, orderItem.Quantity,
            orderItem.Price);
    }
}