using Microsoft.AspNetCore.Mvc;
using RetailMaster.Services;

namespace RetailMaster.Controllers;

[ApiController]
[Route("product-categories")]
public class ProductCategoryController: ControllerBase
{
    private readonly ProductCategoryService _productCategoryService;

    public ProductCategoryController(ProductCategoryService productCategoryService)
    {
        _productCategoryService = productCategoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _productCategoryService.GetAllProductCategoriesAsync();
        return Ok(categories);
    }
}