namespace Ordering.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentationServices(this IServiceCollection serviceCollection)
    {
        
    }
    
    public static WebApplication UseApiServices(this WebApplication app)
    {
        return app;
    }
}