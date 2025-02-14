namespace PrintRelayServer.Shared.Contracts.Base;

public class PaginatedResult<TEntity>
{
    public long TotalCount { get; set; } = 0;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; } = 0;
    public IList<TEntity> Results { get; set; } = new List<TEntity>();
}