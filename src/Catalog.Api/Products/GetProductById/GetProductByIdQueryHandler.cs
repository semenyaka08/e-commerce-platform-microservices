using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Api.Models;
using Marten;

namespace Catalog.Api.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);   
    
public class GetProductByIdQueryHandler(ILogger<GetProductByIdQueryHandler> logger, IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching data with id {id}", query.Id);

        var result = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (result == null)
            throw new NotFoundException($"Product with id {query.Id} was not found");

        return new GetProductByIdResult(result);
    }
}