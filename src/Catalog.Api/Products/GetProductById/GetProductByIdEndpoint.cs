using Carter;
using Catalog.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.GetProductById;

public record GetProductByIdRequest(Guid Id);

public record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product", async ([AsParameters] GetProductByIdRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(request.Id));

            var response = new GetProductByIdResponse(result.Product);

            return Results.Ok(response);
        }).WithName("GetProductById")
        .Produces<GetProductByIdResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}