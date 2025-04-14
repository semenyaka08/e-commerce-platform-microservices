using Carter;
using Ordering.Api.Middlewares;

namespace Ordering.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCarter();
    }
    
    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.MapCarter();
        return app;
    }
}