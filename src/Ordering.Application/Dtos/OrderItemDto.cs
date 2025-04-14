namespace Ordering.Application.Dtos;

public record OrderItemDto(Guid OrderId, int Quantity, decimal Price);