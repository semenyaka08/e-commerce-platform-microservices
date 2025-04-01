using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}