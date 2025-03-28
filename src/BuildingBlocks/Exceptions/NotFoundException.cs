namespace BuildingBlocks.Exceptions;

public abstract class NotFoundException(string name, object key) : Exception($"Entity: {name} with key: {key} was not found");