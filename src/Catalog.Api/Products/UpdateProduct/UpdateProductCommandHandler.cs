using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Api.Models;
using Marten;

namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string Description, string ImageFile, decimal Price, string Category) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSucceeded);

public class UpdateProductCommandHandler(ILogger<UpdateProductCommandHandler> logger, IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product == null)
            throw new NotFoundException($"Product with id {command.Id} was not found");

        product.Name = command.Name;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;
        product.Category = product.Category;
        
        session.Update(product);

        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}