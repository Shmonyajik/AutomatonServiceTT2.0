using Warehouse.Domain.Tools.Exceptions;

namespace Warehouse.Domain.Models;

public class StorageProduct : IEquatable<StorageProduct>
{
    private object _locker = new();

    public StorageProduct(long storageId, long productId)
    {
        StorageId = storageId;
        ProductId = productId;
    }

    public long StorageId { get; }
    public long ProductId { get; }
    public int Quantity { get; private set; } = 1;

    public void AddQuantity(int quantity)
    {
        lock (_locker)
        {
            if (Quantity + quantity < 0)
                throw new DomainException("Product quantity can't be negative");

            Quantity += quantity;
        }
    }

    public bool Equals(StorageProduct? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return StorageId == other.StorageId && ProductId == other.ProductId;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((StorageProduct)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(StorageId, ProductId);
    }
}