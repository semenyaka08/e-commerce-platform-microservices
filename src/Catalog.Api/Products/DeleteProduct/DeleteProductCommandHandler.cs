﻿using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;
using FluentValidation;
using Marten;

namespace Catalog.Api.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSucceeded);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}

public class DeleteProductCommandHandler(ILogger<DeleteProductCommandHandler> logger,IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting product with id {id}", command.Id);

        var productToDelete = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (productToDelete == null)
            throw new ProductNotFoundException(nameof(Product), command.Id);
        
        session.Delete(productToDelete);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}