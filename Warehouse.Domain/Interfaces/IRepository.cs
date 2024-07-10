using System.Linq.Expressions;
using Warehouse.Domain.Models;

namespace Warehouse.Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task<bool> DeleteAsync(T entity);

    Task<T?> FindByIdAsync(long id, bool includeDetails = true);
    Task<T?> FindAsync(Expression<Func<T, bool>> expression, bool includeDetails = true);

    Task<List<T>> GetAllAsync(bool includeDetails = true);
}