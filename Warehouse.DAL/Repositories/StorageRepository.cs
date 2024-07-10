using Microsoft.EntityFrameworkCore;
using Warehouse.DAL.Extensions;
using Warehouse.Domain.Models;

namespace Warehouse.DAL.Repositories;

public class StorageRepository : BaseRepository<Storage>
{
    public StorageRepository(WarehouseDbContext dbContext)
        : base(dbContext)
    {
    }

    public override Task<Storage?> FindByIdAsync(long id, bool includeDetails = true)
    {
        return DbSet
            .IncludeDetails(includeDetails)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}