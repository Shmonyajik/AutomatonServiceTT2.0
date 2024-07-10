using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models;

namespace Warehouse.DAL.Repositories;

public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(WarehouseDbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();
    }

    protected DbSet<TEntity> DbSet => _dbSet;

    public virtual async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public virtual Task<bool> DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        return Task.FromResult(true);
    }

    public virtual async Task<TEntity?> FindByIdAsync(long id, bool includeDetails = true)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression,
        bool includeDetails = true)
    {
        return await DbSet.SingleOrDefaultAsync(expression);
    }

    public virtual async Task<List<TEntity>> GetAllAsync(bool includeDetails = true)
    {
        return await DbSet.ToListAsync();
    }
}