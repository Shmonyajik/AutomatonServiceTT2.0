using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Models;

namespace Warehouse.DAL.Configuration;

public class StorageProductConfiguration : IEntityTypeConfiguration<StorageProduct>
{
    public void Configure(EntityTypeBuilder<StorageProduct> builder)
    {
        builder.ToTable("StorageProduct");
        builder.HasKey(sp => new { sp.ProductId, sp.StorageId });
        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(sp => sp.ProductId)
            .IsRequired();
        builder.HasOne<Storage>()
            .WithMany(s => s.Products)
            .HasForeignKey(sp => sp.StorageId)
            .IsRequired();
    }
}