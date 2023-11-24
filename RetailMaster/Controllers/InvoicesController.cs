using Microsoft.AspNetCore.Mvc;
using RetailMaster.DTO;
using RetailMaster.Models;
using RetailMaster.Services;

namespace RetailMaster.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly InvoiceService _invoiceService;

    public InvoicesController(InvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var invoices = await _invoiceService.GetAllInvoicesAsync();
        return Ok(invoices);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
        if (invoice == null)
        {
            return NotFound();
        }
        return Ok(invoice);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceDto createInvoiceDto)
    {
        Console.WriteLine("TEST");
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var invoice = new Invoice
        {
            ShopId = createInvoiceDto.ShopId,
            Note = createInvoiceDto.Note,
        };

        var createdInvoice = await _invoiceService.CreateInvoiceAsync(invoice);
        
        return CreatedAtAction(nameof(GetById), new { id = createdInvoice.InvoiceId }, createdInvoice);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateInvoiceDto updateInvoiceDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var updatedInvoice = await _invoiceService.UpdateInvoiceAsync(id, updateInvoiceDto);
        if (updatedInvoice == null)
        {
            return NotFound();
        }
        return Ok(updatedInvoice);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _invoiceService.RemoveInvoiceAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}
