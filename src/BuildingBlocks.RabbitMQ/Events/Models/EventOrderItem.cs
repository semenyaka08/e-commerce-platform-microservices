namespace BuildingBlocks.RabbitMQ.Events.Models;

public record EventOrderItem(Guid OrderId, Guid ProductId, int Quantity, decimal Price);