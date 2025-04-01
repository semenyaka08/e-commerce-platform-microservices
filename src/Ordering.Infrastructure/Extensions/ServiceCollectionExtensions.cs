using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Ordering.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        return serviceCollection;
    }
}