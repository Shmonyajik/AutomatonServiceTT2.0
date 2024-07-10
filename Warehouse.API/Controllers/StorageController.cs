using Microsoft.AspNetCore.Mvc;
using Warehouse.API.Services;
using Warehouse.Shared.DTOs.Storages;

namespace Warehouse.API.Controllers;

public class StorageController : BaseApiController
{
    private readonly StorageService _storageService;

    public StorageController(StorageService storageService)
    {
        _storageService = storageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _storageService.GetAllAsync();

        if (!response.Success) return BadRequest(response.ErrorMessage);

        return Ok(response.Resource);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        var response = await _storageService.GetAsync(id);

        if (!response.Success) return BadRequest(response.ErrorMessage);

        return Ok(response.Resource);
    }

    [HttpPost]
    public async Task<IActionResult> Post(StoragePostDto storagePostDto)
    {
        var response = await _storageService.SaveAsync(storagePostDto);

        if (!response.Success) return BadRequest(response.ErrorMessage);

        return Ok(response.Resource);
    }

    [HttpPatch("{id:long}/increaseProductQuantity")]
    public async Task<IActionResult> IncreaseQuantity(long id, StorageIncreaseProductQuantityDto inputDto)
    {
        var response = await _storageService.IncreaseProductQuantity(id, inputDto);

        if (!response.Success) return BadRequest(response.ErrorMessage);

        return Ok(response.Resource);
    }

    [HttpPost("{id:long}/addProduct")]
    public async Task<IActionResult> AddProduct(long id, AddProductDto inputDto)
    {
        var response = await _storageService.AddProduct(id, inputDto);

        if (!response.Success) return BadRequest(response.ErrorMessage);

        return Ok(response.Resource);
    }
}