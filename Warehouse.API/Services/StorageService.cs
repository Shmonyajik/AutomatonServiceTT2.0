using AutoMapper;
using Warehouse.API.Services.Communication;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models;
using Warehouse.Shared.DTOs.Storages;

namespace Warehouse.API.Services;

public class StorageService : BaseService<Storage>
{
    private readonly ILogger<StorageService> _logger;

    public StorageService(IRepository<Storage> repository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<StorageService> logger)
        : base(repository, unitOfWork, mapper)
    {
        _logger = logger;
    }

    public async Task<ServiceResponse<List<StorageDto>>> GetAllAsync()
    {
        _logger.LogInformation("Request to StorageService: GetAllAsync()");
        var storages = await Repository.GetAllAsync();

        var storageDtos = Mapper.Map<List<Storage>, List<StorageDto>>(storages);

        _logger.LogInformation("Returned successful response from StorageService: GetAllAsync()");
        return new ServiceResponse<List<StorageDto>>(storageDtos);
    }

    public async Task<ServiceResponse<StorageDto>> GetAsync(long id)
    {
        _logger.LogInformation("Request to StorageService: GetAsync({@Id})", id);
        var storage = await Repository.FindByIdAsync(id);
        if (storage == null)
        {
            _logger.LogError("Returned bad response from StorageService: GetAsync({@Id}). Storage wasn't found", id);
            return new ServiceResponse<StorageDto>("Storage wasn't found");
        }

        var storageDto = Mapper.Map<Storage, StorageDto>(storage);

        _logger.LogInformation("Returned successful response from StorageService: GetAsync({@Id})", id);
        return new ServiceResponse<StorageDto>(storageDto);
    }

    public async Task<ServiceResponse<StorageDto>> SaveAsync(StoragePostDto storagePostDto)
    {
        _logger.LogInformation("Request to StorageService: SaveAsync({@Dto})", storagePostDto);
        var storage = Mapper.Map<StoragePostDto, Storage>(storagePostDto);

        try
        {
            await Repository.AddAsync(storage);
            await UnitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Returned bad response from StorageService: SaveAsync({@Dto}). An error occurred while saving the storage: {@Message}",
                storagePostDto, e.Message);
            return new ServiceResponse<StorageDto>($"An error occurred while saving the storage: {e.Message}");
        }

        var storageDto = Mapper.Map<Storage, StorageDto>(storage);

        _logger.LogInformation("Returned successful response from StorageService: SaveAsync({@Dto})", storageDto);
        return new ServiceResponse<StorageDto>(storageDto);
    }

    public async Task<ServiceResponse<StorageDto>> IncreaseProductQuantity(long id,
        StorageIncreaseProductQuantityDto inputDto)
    {
        _logger.LogInformation("Request to StorageService: IncreaseProductQuantity({Id}, {@Dto})", id, inputDto);
        var storage = await Repository.FindByIdAsync(id);
        if (storage == null)
        {
            _logger.LogError(
                "Returned bad response from StorageService: IncreaseProductQuantity({Id}, {@Dto}). Storage wasn't found",
                id, inputDto);
            return new ServiceResponse<StorageDto>("Storage wasn't found");
        }

        try
        {
            storage.IncreaseProductQuantity(inputDto.ProductId, inputDto.Quantity);
            await UnitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Returned bad response from StorageService: IncreaseProductQuantity({Id}, {@Dto}). An error occurred while increasing the product quantity: {@Message}",
                id, inputDto, e.Message);
            return new ServiceResponse<StorageDto>(
                $"An error occurred while increasing the product quantity: {e.Message}");
        }

        var storageDto = Mapper.Map<Storage, StorageDto>(storage);

        _logger.LogInformation(
            "Returned successful response from StorageService: IncreaseProductQuantity({Id}, {@Dto})", id, inputDto);
        return new ServiceResponse<StorageDto>(storageDto);
    }

    public async Task<ServiceResponse<StorageDto>> AddProduct(long id, AddProductDto inputDto)
    {
        var storage = await Repository.FindByIdAsync(id);
        if (storage == null)
        {
            _logger.LogError(
                "Returned bad response from StorageService: AddProduct({Id}, {@Dto}). Storage wasn't found", id,
                inputDto);
            return new ServiceResponse<StorageDto>("Storage wasn't found");
        }

        try
        {
            storage.AddProduct(inputDto.ProductId);
            await UnitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Returned bad response from StorageService: AddProduct({Id}, {@Dto}). An error occurred while adding the new product: {@Message}",
                id, inputDto, e.Message);
            return new ServiceResponse<StorageDto>($"An error occurred while adding the new product: {e.Message}");
        }

        var storageDto = Mapper.Map<Storage, StorageDto>(storage);

        _logger.LogInformation("Returned successful response from StorageService: AddProductDto({Id}, {@Dto})", id,
            inputDto);
        return new ServiceResponse<StorageDto>(storageDto);
    }
}