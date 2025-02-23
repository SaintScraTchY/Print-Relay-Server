using System.Linq.Expressions;
using PrintRelayServer.Domain.Base;
using PrintRelayServer.Shared.Contracts.Base;

namespace PrintRelayServer.Domain.IRepositories;

public interface IRepository<TEntity>
{
    Task<TEntity> AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    Task<TEntity?> GetByIdAsync(Guid id);

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,string? includes = null);

    Task<PaginatedResult<TEntity>> GetPaginatedAsync(int pageNumber, int pageSize,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,string? includes = null);
    
    void Update(TEntity entity);
    Task SoftDeleteAsync(Guid id);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    Task<bool> SaveChangesAsync(bool useTransaction = false);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);
}
