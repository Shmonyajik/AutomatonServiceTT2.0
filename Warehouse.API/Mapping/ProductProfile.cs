using AutoMapper;
using Warehouse.Domain.Models;
using Warehouse.Shared.DTOs.Products;

namespace Warehouse.API.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
    }
}