using BuildingBlocks.Exceptions;

namespace Catalog.Api.Exceptions;

public class ProductNotFoundException(string name, object key) : NotFoundException(name, key);