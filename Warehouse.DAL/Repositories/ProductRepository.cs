using Warehouse.Domain.Models;

namespace Warehouse.DAL.Repositories;

public class ProductRepository : BaseRepository<Product>
{
    public ProductRepository(WarehouseDbContext dbContext)
        : base(dbContext)
    {
    }
}