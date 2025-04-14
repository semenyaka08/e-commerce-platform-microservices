using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<OrderingDbContext>();

        await context.Database.MigrateAsync();

        await SeedAsync(context);
    }
    
    private static async Task SeedAsync(OrderingDbContext context)
    {
        await SeedOrdersWithItemsAsync(context);
    }

    private static async Task SeedOrdersWithItemsAsync(OrderingDbContext context)
    {
        if (!await context.Orders.AnyAsync())
        {
            await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
            await context.SaveChangesAsync();
        }
    }
}