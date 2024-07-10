using Microsoft.AspNetCore.Mvc;
using Warehouse.API.Services;

namespace Warehouse.API.Controllers;

public class ProductController : BaseApiController
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _productService.GetAllAsync();

        if (!response.Success) return BadRequest(response.ErrorMessage);

        return Ok(response.Resource);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        var response = await _productService.GetAsync(id);

        if (!response.Success) return BadRequest(response.ErrorMessage);

        return Ok(response.Resource);
    }
}