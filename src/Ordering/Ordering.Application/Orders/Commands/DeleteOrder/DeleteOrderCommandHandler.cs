using BuildingBlocks.CQRS;
using Ordering.Application.Exceptions;
using Ordering.Domain.Abstractions.Repositories;
using Ordering.Domain.Models;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IOrderRepository orderRepository) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetOrderByIdAsync(request.Id);

        if (order == null)
            throw new OrderNotFoundException(nameof(Order), request.Id);

        await orderRepository.DeleteOrder(order);

        return new DeleteOrderResult(true);
    }
}