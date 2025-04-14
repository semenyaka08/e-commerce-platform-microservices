using BuildingBlocks.CQRS;
using Catalog.Api.Models;
using Marten;

namespace Catalog.Api.Products.GetProductsByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

public record GetProductsByCategoryResult(IEnumerable<Product> Products);

public class GetProductsByCategoryQueryHandler(ILogger<GetProductsByCategoryQueryHandler> logger, IDocumentSession session) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching products with category {category}", query.Category);

        var result = await session.Query<Product>()
            .Where(z => z.Category.Contains(query.Category))
            .ToListAsync(token: cancellationToken);

        return new GetProductsByCategoryResult(result);
    }
}