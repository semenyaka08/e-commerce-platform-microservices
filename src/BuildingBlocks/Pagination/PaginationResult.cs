namespace BuildingBlocks.Pagination;

public class PaginationResult<TEntity>(int pageNumber, int pageSize, long itemsCount, List<TEntity> data) where TEntity : class
{
    public int PageNumber { get; set; } = pageNumber;
    
    public int PageSize { get; set; } = pageSize;
    
    public long ItemsCount { get; set; } = itemsCount;
    
    public List<TEntity> Data { get; set; } = data;
}