using BuildingBlocks.CQRS;
using Ordering.Domain.Abstractions.Repositories;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IOrderRepository orderRepository) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateOrder(command);

        var dbResponse = await orderRepository.AddOrderAsync(order);

        return new CreateOrderResult(dbResponse);
    }

    private Order CreateOrder(CreateOrderCommand command)
    {
        var shippingAddress = Address.Of(command.Order.ShippingAddress.FirstName,
            command.Order.ShippingAddress.LastName,
            command.Order.ShippingAddress.EmailAddress,
            command.Order.ShippingAddress.AddressLine,
            command.Order.ShippingAddress.Country,
            command.Order.ShippingAddress.State,
            command.Order.ShippingAddress.ZipCode);
        
        var billingAddress = Address.Of(command.Order.BillingAddress.FirstName,
            command.Order.BillingAddress.LastName,
            command.Order.BillingAddress.EmailAddress,
            command.Order.BillingAddress.AddressLine,
            command.Order.BillingAddress.Country,
            command.Order.BillingAddress.State,
            command.Order.BillingAddress.ZipCode);

        var payment = Payment.Of(command.Order.Payment.CardNumber,
            command.Order.Payment.CardNumber,
            command.Order.Payment.Expiration,
            command.Order.Payment.Cvv,
            command.Order.Payment.PaymentMethod);

        var order = Order.Create(CustomerId.Of(Guid.NewGuid()),
            OrderName.Of(command.Order.OrderName),
            shippingAddress,
            billingAddress,
            payment
            );

        foreach (var item in command.Order.OrderItems)
        {
            order.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
        }

        return order;
    }
}