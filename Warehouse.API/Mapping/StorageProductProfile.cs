using AutoMapper;
using Warehouse.Domain.Models;
using Warehouse.Shared.DTOs.StorageProducts;

namespace Warehouse.API.Mapping;

public class StorageProductProfile : Profile
{
    public StorageProductProfile()
    {
        CreateMap<StorageProduct, StorageProductDto>();
    }
}