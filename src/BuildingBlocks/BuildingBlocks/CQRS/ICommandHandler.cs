using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
    where TRequest : ICommand<TResponse>;