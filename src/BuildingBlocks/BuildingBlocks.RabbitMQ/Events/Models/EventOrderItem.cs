namespace BuildingBlocks.RabbitMQ.Events.Models;

public record EventOrderItem(Guid OrderId, int Quantity, decimal Price);