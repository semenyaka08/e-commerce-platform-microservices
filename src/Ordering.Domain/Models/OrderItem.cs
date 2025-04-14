using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models;

public class OrderItem : Entity<OrderItemId>
{
    public OrderItem(OrderId orderId, int quantity, decimal price)
    {
        Id = OrderItemId.Of(Guid.NewGuid());
        OrderId = orderId;
        Quantity = quantity;
        Price = price;
    }
    
    public OrderId OrderId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
}