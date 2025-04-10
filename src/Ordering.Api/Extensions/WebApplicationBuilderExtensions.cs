using Carter;

namespace Ordering.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCarter();
    }
    
    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        return app;
    }
}