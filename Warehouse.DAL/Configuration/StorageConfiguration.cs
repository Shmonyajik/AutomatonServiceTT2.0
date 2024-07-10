using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Models;

namespace Warehouse.DAL.Configuration;

public class StorageConfiguration : IEntityTypeConfiguration<Storage>
{
    public void Configure(EntityTypeBuilder<Storage> builder)
    {
        builder.ToTable("Storage");
        builder.HasKey(s => s.Id);
        builder.HasMany(s => s.Employees)
            .WithMany(e => e.Storages);

        builder.Ignore(s => s.EmployeesDictionary);
        builder.Ignore(s => s.ProductsDictionary);
    }
}