namespace CoachBoard.Domain.Shared;
public class PaginatedResult<T>
{
    public PaginatedResult(List<T> data, int page, int pageSize, int total)
    {
        Data = data;
        Page = page;
        PageSize = pageSize;
        Total = total;
    }

    public List<T> Data { get; } = [];    
    public int Page { get; }
    public int PageSize { get; }
    public int Total { get; }
    public bool HasNextPage => Page * PageSize < Total;
    public bool HasPreviousPage => Page > 1;
}
