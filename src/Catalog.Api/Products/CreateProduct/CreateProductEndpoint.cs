using Carter;
using MediatR;

namespace Catalog.Api.Products.CreateProduct;

public record CreateProductRequest(string Name, string Description, string ImageFile, decimal Price, string Category);

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = ProductMapper.FromRequestToCommand(request);

            var result = await sender.Send(command);

            var response = ProductMapper.FromResultToResponse(result);

            return Results.Created($"/products/{result.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");;
    }
}