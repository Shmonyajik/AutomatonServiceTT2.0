using Warehouse.Domain.Interfaces;

namespace Warehouse.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly WarehouseDbContext _dbContext;

    public UnitOfWork(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}