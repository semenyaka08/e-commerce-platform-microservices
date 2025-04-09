using BuildingBlocks.CQRS;
using Microsoft.Extensions.Logging;
using Ordering.Application.Extensions;
using Ordering.Domain.Abstractions.Repositories;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerQueryHandler(ILogger<GetOrdersByCustomerQueryHandler> logger, IOrderRepository orderRepository) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching orders for customer with Id: {CustomerId}", request.CustomerId);
        
        var orders = await orderRepository.GetOrdersByCustomerAsync(request.CustomerId);

        var mappedOrders = orders.Select(z => z.ToDto()).ToList();

        return new GetOrdersByCustomerResult(mappedOrders);
    }
}