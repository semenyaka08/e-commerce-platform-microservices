using BuildingBlocks.CQRS;
using Carter;
using MediatR;

namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductRequest(
    Guid Id,
    string Name,
    string Description,
    string ImageFile,
    decimal Price,
    string Category);

public record UpdateProductResponse(bool IsSucceeded);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            var updateCommand = ProductMapper.FromRequestToCommand(request);
            
            var result = await sender.Send(updateCommand);

            return Results.Ok(new UpdateProductResponse(result.IsSucceeded));
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Product")
        .WithDescription("Update Product");;
    }
}