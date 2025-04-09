using Ordering.Domain.Models;

namespace Ordering.Domain.Abstractions.Repositories;

public interface IOrderRepository
{
    public Task<Guid> AddOrderAsync(Order order);

    public Task<Order?> GetOrderByIdAsync(Guid id);

    public Task SaveChangesAsync();

    public Task DeleteOrder(Order order);

    public Task<List<Order>> GetOrdersByNameAsync(string name);
    
    public Task<List<Order>> GetOrdersByCustomerAsync(Guid customerId);
    
    public Task<List<Order>> GetOrdersAsync(int pageSize, int pageNumber);
}