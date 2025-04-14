using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Abstractions.Repositories;
using Ordering.Domain.Models;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository(OrderingDbContext context) : IOrderRepository
{
    public async Task<Guid> AddOrderAsync(Order order)
    {
        await context.Orders.AddAsync(order);

        await context.SaveChangesAsync();

        return order.Id.Value;
    }

    public async Task<Order?> GetOrderByIdAsync(Guid id)
    {
        var order = await context.Orders.FirstOrDefaultAsync(z => z.Id.Value == id);

        return order;
    }

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();

    public async Task DeleteOrder(Order order)
    {
        context.Orders.Remove(order);

        await context.SaveChangesAsync();
    }

    public async Task<List<Order>> GetOrdersByNameAsync(string name)
    {
        var orders = await context.Orders
            .AsNoTracking()
            .Include(z => z.OrderItems)
            .Where(z => z.OrderName.Value.Contains(name))
            .ToListAsync();
    
        return orders;
    }

    public async Task<List<Order>> GetOrdersByCustomerAsync(Guid customerId)
    {
        var orders = await context.Orders
            .AsNoTracking()
            .Include(z => z.OrderItems)
            .Where(z => z.CustomerId.Value == customerId)
            .ToListAsync();
    
        return orders;
    }

    public async Task<List<Order>> GetOrdersAsync(int pageSize, int pageNumber)
    {
        var orders = await context.Orders
            .AsNoTracking()
            .Include(z => z.OrderItems)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return orders;
    }
}