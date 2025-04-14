using Carter;
using Catalog.Api.Models;
using MediatR;

namespace Catalog.Api.Products.GetProducts;

public record GetProductsRequest(int? PageNumber, int? PageSize);

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request ,ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery(request.PageNumber, request.PageSize));

            var response = new GetProductsResponse(result.Products);

            return Results.Ok(response);
        }).WithName("GetProducts")
        .Produces<GetProductsResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");;
    }
}