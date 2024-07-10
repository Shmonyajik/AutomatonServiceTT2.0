namespace Warehouse.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}