using BuildingBlocks.Exceptions;

namespace Basket.Api.Exceptions;

public class BasketNotFoundException(string name, object key) : NotFoundException(name, key);