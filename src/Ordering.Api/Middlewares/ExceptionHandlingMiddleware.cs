using FluentValidation;
using Ordering.Application.Exceptions;

namespace Ordering.Api.Middlewares;

public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (OrderNotFoundException exception)
        {
            context.Response.StatusCode = 404;
                
            await context.Response.WriteAsync(exception.Message);
            logger.LogWarning(exception.Message);
        }
        catch (ValidationException exception)
        {
            context.Response.StatusCode = 400;

            var errors = exception.Errors
                .Select(z => new { z.PropertyName, z.ErrorMessage })
                .ToList();

            var response = new
            {
                Message = "Validation failed",
                Errors = errors
            };
                
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}