using System.Reflection;
using BuildingBlocks.Behaviors;
using BuildingBlocks.RabbitMQ.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        serviceCollection.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
        
        return serviceCollection;
    }
}