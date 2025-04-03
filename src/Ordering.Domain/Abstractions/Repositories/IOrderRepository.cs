using Ordering.Domain.Models;

namespace Ordering.Domain.Abstractions.Repositories;

public interface IOrderRepository
{
    public Task<Guid> AddOrderAsync(Order order);

    public Task<Order?> GetOrderByIdAsync(Guid id);

    public Task SaveChangesAsync();

    public Task DeleteOrder(Order order);
}