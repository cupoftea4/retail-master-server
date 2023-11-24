using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RetailMaster.DTO;
using RetailMaster.Models;

namespace RetailMaster.Services;

public class InvoiceProductService
{
    private readonly RetailMasterContext _context;
    private readonly IMapper _mapper;

    public InvoiceProductService(RetailMasterContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<InvoiceProductDto>> GetAllInvoiceProductsForInvoice(int invoiceId)
    {
        var invoiceProducts = await _context.InvoiceProducts
            .Where(ip => ip.InvoiceId == invoiceId)
            .Include(ip => ip.Product) // Assuming you have a navigation property to Product
            .ToListAsync();

        return _mapper.Map<List<InvoiceProductDto>>(invoiceProducts);
    }
    
    public async Task<InvoiceProductDto?> AddProductToInvoice(int invoiceId, CreateInvoiceProductDto createDto)
    {
        var invoice = await _context.Invoices.Include(i => i.InvoiceProducts)
            .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId);
        if (invoice == null)
        {
            return null;
        }

        var product = await _context.Products.FindAsync(createDto.ProductId);
        if (product == null)
        {
            return null;
        }

        var invoiceProduct = new InvoiceProduct
        {
            InvoiceId = invoiceId,
            ProductId = createDto.ProductId,
            Quantity = createDto.Quantity,
            RetailPrice = createDto.RetailPrice,
            WholeReceiptProductPrice = createDto.WholeReceiptProductPrice
        };

        _context.InvoiceProducts.Add(invoiceProduct);
        await _context.SaveChangesAsync();

        return _mapper.Map<InvoiceProductDto>(invoiceProduct);
    }
    
    public async Task<InvoiceProductDto?> UpdateProductInInvoice(int invoiceId, int invoiceProductId, UpdateInvoiceProductDto updateDto)
    {
        var invoiceProduct = await _context.InvoiceProducts
            .FirstOrDefaultAsync(ip => ip.InvoiceProductId == invoiceProductId && ip.InvoiceId == invoiceId);

        if (invoiceProduct == null)
        {
            return null;
        }

        // Update fields if provided in DTO
        if (updateDto.ProductId.HasValue)
        {
            invoiceProduct.ProductId = updateDto.ProductId.Value;
        }
        if (updateDto.Quantity.HasValue)
        {
            invoiceProduct.Quantity = updateDto.Quantity.Value;
        }
        if (updateDto.RetailPrice.HasValue)
        {
            invoiceProduct.RetailPrice = updateDto.RetailPrice.Value;
        }
        if (updateDto.WholeReceiptProductPrice.HasValue)
        {
            invoiceProduct.WholeReceiptProductPrice = updateDto.WholeReceiptProductPrice.Value;
        }

        await _context.SaveChangesAsync();

        return _mapper.Map<InvoiceProductDto>(invoiceProduct);
    }
    
    public async Task<bool> RemoveProductFromInvoice(int invoiceId, int invoiceProductId)
    {
        var invoiceProduct = await _context.InvoiceProducts
            .FirstOrDefaultAsync(ip => ip.InvoiceProductId == invoiceProductId && ip.InvoiceId == invoiceId);

        if (invoiceProduct == null)
        {
            return false;
        }

        _context.InvoiceProducts.Remove(invoiceProduct);
        await _context.SaveChangesAsync();

        return true;
    }

}