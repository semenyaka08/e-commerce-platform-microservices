using BuildingBlocks.CQRS;
using Microsoft.Extensions.Logging;
using Ordering.Application.Extensions;
using Ordering.Domain.Abstractions.Repositories;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrderByNameQueryHandler(ILogger logger, IOrderRepository orderRepository) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("fetching order with name: {Name}", request.Name);

        var order = await orderRepository.GetOrdersByNameAsync(request.Name);

        var mappedOrders = order.Select(z=>z.ToDto()).ToList();
        
        return new GetOrdersByNameResult(mappedOrders);
    }
}