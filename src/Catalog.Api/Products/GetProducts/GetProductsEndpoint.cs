using Carter;
using Catalog.Api.Models;
using MediatR;

namespace Catalog.Api.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());

            var response = new GetProductsResponse(result.Products);

            return Results.Ok(response);
        }).WithName("GetProducts")
        .Produces<GetProductsResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");;
    }
}