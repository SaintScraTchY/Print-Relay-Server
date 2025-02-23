using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.IRepositories;
using PrintRelayServer.Infrastructure.Contexts;
using PrintRelayServer.Shared.Contracts.Base;

namespace PrintRelayServer.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly PrintRelayContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(PrintRelayContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    // Add an entity
    public async Task<TEntity> AddAsync(TEntity entity)
        => (await _dbSet.AddAsync(entity)).Entity;

    // Add multiple entities
    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        => await _dbSet.AddRangeAsync(entities);

    // Get an entity by ID
    public async Task<TEntity?> GetByIdAsync(Guid id)
        => await _dbSet.FindAsync(id);

    // Get all entities (with optional filters, ordering, and pagination)
    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,string? includes = null)
    {
        IQueryable<TEntity> query = _dbSet;

        // Apply filter
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includes != null)
        {
            var includeList = includes.Split(",");
            query = includeList.Aggregate(query, (current, include) => current.Include(include));
        }
        
        // Apply ordering
        if (orderBy != null)
        {
            query = orderBy(query);
        }
        return await query.ToListAsync();
    }
    
    public async Task<PaginatedResult<TEntity>> GetPaginatedAsync(int pageNumber, int pageSize,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,string? includes = null)
    {
        IQueryable<TEntity> query = _dbSet;

        // Apply filter
        if (filter != null)
        {
            query = query.Where(filter);
        }
        
        if (includes != null)
        {
            var includeList = includes.Split(",");
            query = includeList.Aggregate(query, (current, include) => current.Include(include));
        }

        // Apply ordering
        if (orderBy != null)
        {
            query = orderBy(query);
        }

        // Apply pagination
        var totalCount = await query.LongCountAsync();

        if (totalCount == 0)
        {
            return new PaginatedResult<TEntity>()
            {
                Results = new List<TEntity>(),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = 0
            };
        }
        
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        var data=  await query.ToListAsync();
        
        return new PaginatedResult<TEntity>()
        {
            Results = data,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages 
        };
    }

// Update an entity
    public void Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

// Soft delete an entity (if it implements ISoftDeletable)
    public async Task SoftDeleteAsync(Guid id)
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
    public async Task<bool> SaveChangesAsync(bool useTransaction = false)
    {
        if (useTransaction)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
        else
        {
            return await _context.SaveChangesAsync() > 0;
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