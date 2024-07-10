using AutoMapper;
using Warehouse.API.Services.Communication;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models;
using Warehouse.Shared.DTOs.Products;

namespace Warehouse.API.Services;

public class ProductService : BaseService<Product>
{
    private readonly ILogger<ProductService> _logger;

    public ProductService(IRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductService> logger)
        : base(repository, unitOfWork, mapper)
    {
        _logger = logger;
    }

    public async Task<ServiceResponse<List<ProductDto>>> GetAllAsync()
    {
        _logger.LogInformation("Request to ProductService: GetAllAsync()");
        var products = await Repository.GetAllAsync();

        var productDtos = Mapper.Map<List<Product>, List<ProductDto>>(products);

        _logger.LogInformation("Returned successful response from ProductService: GetAllAsync()");
        return new ServiceResponse<List<ProductDto>>(productDtos);
    }

    public async Task<ServiceResponse<ProductDto>> GetAsync(long id)
    {
        _logger.LogInformation("Request to ProductService: GetAsync({@Id})", id);
        var product = await Repository.FindByIdAsync(id);
        if (product == null)
        {
            _logger.LogError("Returned bad response from ProductService: GetAsync({@Id}). Product wasn't found", id);
            return new ServiceResponse<ProductDto>("Product wasn't found");
        }

        var productDto = Mapper.Map<Product, ProductDto>(product);

        _logger.LogInformation("Returned successful response from ProductService: GetAsync({@Id})", id);
        return new ServiceResponse<ProductDto>(productDto);
    }
}