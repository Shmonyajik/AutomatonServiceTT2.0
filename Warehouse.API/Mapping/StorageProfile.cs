using AutoMapper;
using Warehouse.Domain.Models;
using Warehouse.Shared.DTOs.Storages;

namespace Warehouse.API.Mapping;

public class StorageProfile : Profile
{
    public StorageProfile()
    {
        CreateMap<Storage, StorageDto>();

        CreateMap<StoragePostDto, Storage>();
    }
}