﻿using BuildingBlocks.CQRS;
using Catalog.Api.Models;
using Marten;

namespace Catalog.Api.Products.GetProducts;

public record GetProductsQuery : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger, IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching products from database {query}", query);

        var result = await session.Query<Product>().ToListAsync(token: cancellationToken);

        return new GetProductsResult(result);
    }
}