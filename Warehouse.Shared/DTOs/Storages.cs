using Warehouse.Shared.DTOs.StorageProducts;

namespace Warehouse.Shared.DTOs.Storages;

public record StorageDto(long Id, IEnumerable<StorageProductDto> Products);

public record StoragePostDto();

public record StorageIncreaseProductQuantityDto(long ProductId, int Quantity);

public record AddProductDto(long ProductId);