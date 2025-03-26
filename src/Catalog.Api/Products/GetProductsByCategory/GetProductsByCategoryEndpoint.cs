using Carter;
using Catalog.Api.Models;
using MediatR;

namespace Catalog.Api.Products.GetProductsByCategory;

public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var result = await sender.Send(new GetProductsByCategoryQuery(category));

            var response = new GetProductsByCategoryResponse(result.Products);

            return Results.Ok(response);
            
        }).WithName("GetProductByCategory")
        .Produces<GetProductsByCategoryResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");;
    }
}