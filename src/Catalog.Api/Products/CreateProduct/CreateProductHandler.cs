using BuildingBlocks.CQRS;

namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, string ImageFile, decimal Price, string Category) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}