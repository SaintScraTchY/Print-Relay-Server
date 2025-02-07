namespace PrintRelayServer.Shared.Contracts.Base;

public class PaginatedResult<TEntity>
{
    public uint TotalCount { get; set; } = 0;
    public uint PageNumber { get; set; }
    public uint PageSize { get; set; }
    public uint TotalPages { get; set; } = 0;
    public IList<TEntity> Results { get; set; } = new List<TEntity>();
}