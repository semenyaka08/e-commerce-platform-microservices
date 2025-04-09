using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Microsoft.Extensions.Logging;
using Ordering.Application.Dtos;
using Ordering.Application.Extensions;
using Ordering.Domain.Abstractions.Repositories;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler(ILogger<GetOrdersQueryHandler> logger, IOrderRepository orderRepository)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching orders, page number: {pageNumber}; page size: {pageSize}",
            request.PaginationRequest.PageNumber, request.PaginationRequest.PageSize);

        var orders = await orderRepository.GetOrdersAsync(request.PaginationRequest.PageSize, request.PaginationRequest.PageNumber);

        var mappedOrders = orders.Select(z=>z.ToDto()).ToList();

        var paginationResult = new PaginationResult<OrderDto>(request.PaginationRequest.PageNumber, request.PaginationRequest.PageSize, mappedOrders.Count, mappedOrders);

        return new GetOrdersResult(paginationResult);
    }
}