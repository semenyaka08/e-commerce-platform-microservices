using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public static class Extensions
{
    public static async Task<IApplicationBuilder> UseMigration(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();

        await dbContext.Database.MigrateAsync();

        return builder;
    } 
}