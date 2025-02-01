using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.IRepositories;
using PrintRelayServer.Infrastructure.Contexts;

namespace PrintRelayServer.Infrastructure.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : Entity<Guid>
{
    private readonly PrintRelayContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(PrintRelayContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

// Add an entity
    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

// Add multiple entities
    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

// Get an entity by ID
    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        return await _dbSet.FindAsync(id);
    }

// Get all entities (with optional filters, ordering, and pagination)
    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? pageNumber = null,
        int? pageSize = null)
    {
        IQueryable<TEntity> query = _dbSet;

        // Apply filter
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // Apply ordering
        if (orderBy != null)
        {
            query = orderBy(query);
        }

        // Apply pagination
        if (pageNumber.HasValue && pageSize.HasValue)
        {
            query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        return await query.ToListAsync();
    }

// Update an entity
    public void Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

// Soft delete an entity (if it implements ISoftDeletable)
    public async Task SoftDeleteAsync(TKey id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is SoftDeleteEntity softDeletableEntity)
        {
            softDeletableEntity.IsDeleted = true;
            _context.Entry(entity).State = EntityState.Modified;
        }
        else
        {
            throw new InvalidOperationException("Entity does not support soft delete.");
        }
    }

// Hard delete an entity
    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

// Hard delete multiple entities
    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

// Save changes (optionally within a transaction)
    public async Task SaveChangesAsync(bool useTransaction = false)
    {
        if (useTransaction)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        else
        {
            await _context.SaveChangesAsync();
        }
    }

// Check if an entity exists
    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _dbSet.AnyAsync(filter);
    }

// Count entities (with optional filter)
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        if (filter != null)
        {
            return await _dbSet.CountAsync(filter);
        }

        return await _dbSet.CountAsync();
    }
}