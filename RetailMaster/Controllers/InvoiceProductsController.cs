using Microsoft.AspNetCore.Mvc;
using RetailMaster.DTO;
using RetailMaster.Services;

namespace RetailMaster.Controllers;

[ApiController]
[Route("invoices")]
public class InvoiceProductsController : ControllerBase
{
    private readonly InvoiceProductService _invoiceProductService;

    public InvoiceProductsController(InvoiceProductService invoiceProductService)
    {
        _invoiceProductService = invoiceProductService;
    }
    
    [HttpGet("{invoiceId:int}/products")]
    public async Task<IActionResult> GetAllInvoiceProductsForInvoice(int invoiceId)
    {
        var invoiceProducts = await _invoiceProductService.GetAllInvoiceProductsForInvoice(invoiceId);
        if (!invoiceProducts.Any())
        {
            return NotFound($"No products found for invoice with ID {invoiceId}.");
        }

        return Ok(invoiceProducts);
    }
    
    [HttpPost("{invoiceId:int}/products")]
    public async Task<IActionResult> AddProductToInvoice(int invoiceId, [FromBody] CreateInvoiceProductDto createDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var invoiceProductDto = await _invoiceProductService.AddProductToInvoice(invoiceId, createDto);
        if (invoiceProductDto == null)
        {
            return NotFound("Invoice or Product not found.");
        }

        return CreatedAtRoute(new { invoiceId = invoiceId, invoiceProductId = invoiceProductDto.InvoiceProductId }, invoiceProductDto);
    }
    
    [HttpDelete("{invoiceId:int}/products/{invoiceProductId:int}")]
    public async Task<IActionResult> DeleteProductFromInvoice(int invoiceId, int invoiceProductId)
    {
        var success = await _invoiceProductService.RemoveProductFromInvoice(invoiceId, invoiceProductId);
        if (!success)
        {
            return NotFound($"InvoiceProduct with ID {invoiceProductId} in Invoice {invoiceId} not found.");
        }

        return NoContent();
    }

    [HttpPatch("{invoiceId:int}/products/{invoiceProductId:int}")]
    public async Task<IActionResult> UpdateProductInInvoice(int invoiceId, int invoiceProductId, [FromBody] UpdateInvoiceProductDto updateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedInvoiceProductDto = await _invoiceProductService.UpdateProductInInvoice(invoiceId, invoiceProductId, updateDto);
        if (updatedInvoiceProductDto == null)
        {
            return NotFound($"InvoiceProduct with ID {invoiceProductId} in Invoice {invoiceId} not found.");
        }

        return Ok(updatedInvoiceProductDto);
    }
}