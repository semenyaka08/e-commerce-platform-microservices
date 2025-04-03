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
        var order = await context.Orders.FirstOrDefaultAsync(z=>z.Id.Value == id);

        return order;
    }

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
    
    public async Task DeleteOrder(Order order)
    {
        context.Orders.Remove(order);

        await context.SaveChangesAsync();
    }
}