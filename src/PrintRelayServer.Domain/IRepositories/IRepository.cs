using System.Linq.Expressions;
using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.IRepositories;

public interface IRepository<TEntity, TKey>
    where TEntity : Entity<Guid>
{
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    Task<TEntity?> GetByIdAsync(TKey id);

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? pageNumber = null,
        int? pageSize = null);

    void Update(TEntity entity);
    Task SoftDeleteAsync(TKey id);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    Task SaveChangesAsync(bool useTransaction = false);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);
}
