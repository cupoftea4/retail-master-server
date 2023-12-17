using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailMaster.DTO;
using RetailMaster.Models;
using RetailMaster.Services;

namespace RetailMaster.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet(Name = "Products")]
    // [Authorize(Roles = "admin,worker,seller")]
    public async Task<IActionResult> Get()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }
    
    [HttpPost(Name = "CreateProduct")]
    public async Task<IActionResult> Create([FromBody] CreateProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var product = new Product
        {
            CategoryId = productDto.CategoryId,
            Name = productDto.Name,
            Barcode = productDto.Barcode
        };

        await _productService.AddProductAsync(product);
        return CreatedAtAction(nameof(Get), new { id = product.ProductId }, product);
    }

    [HttpDelete("{id:int}", Name = "DeleteProduct")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { error = $"Product with ID {id} not found." });
        }
    }
    
    [HttpPatch("{id:int}", Name = "UpdateProduct")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto updateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedProduct = await _productService.UpdateProductAsync(id, updateDto);
            if (updatedProduct == null)
            {
                return NotFound(new { error = $"Product with ID {id} not found." });
            }
            return Ok(updatedProduct);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
}