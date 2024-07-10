using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Models;

namespace Warehouse.DAL;

public class WarehouseDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<StorageProduct> StorageProducts { get; set; }

    public WarehouseDbContext()
    {
    }

    public WarehouseDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        SyncStorageProducts();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SyncStorageProducts();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SyncStorageProducts();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        SyncStorageProducts();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void SyncStorageProducts()
    {
        foreach (var entry in ChangeTracker.Entries()
                     .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Unchanged))
        {
            if (entry.Entity is Storage storage)
            {
                storage.SyncCollections();
            }
        }
    }
}