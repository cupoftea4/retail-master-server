using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using RetailMaster.DTO;
using RetailMaster.Middleware;
using RetailMaster.Models;

namespace RetailMaster.Services;

public class InvoiceService
{
    private readonly RetailMasterContext _context;
    private readonly IMapper _mapper;

    public InvoiceService(RetailMasterContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<InvoiceDto> CreateInvoiceAsync(Invoice invoice)
    {
        try
        {
            invoice.Date = DateTime.UtcNow.ToString("yyyy-MM-dd"); // Setting the date

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            
            var refetchedInvoice = await _context.Invoices
                .Include(i => i.Shop)
                .FirstOrDefaultAsync(i => i.InvoiceId == invoice.InvoiceId);

            return _mapper.Map<InvoiceDto>(refetchedInvoice);
        }
        catch (DbUpdateException ex) when (ex.InnerException is MySqlException mysqlEx && mysqlEx.Number == 1452)
        {
            throw new ForeignKeyConstraintException("Invalid Shop ID or other foreign key constraint violation.", ex);
        }
    }

    public async Task<bool> RemoveInvoiceAsync(int invoiceId)
    {
        var invoice = await _context.Invoices.FindAsync(invoiceId);
        if (invoice == null)
        {
            return false;
        }

        _context.Invoices.Remove(invoice);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<InvoiceDto?> UpdateInvoiceAsync(int invoiceId, UpdateInvoiceDto updateDto)
    {
        var invoice = await _context.Invoices.FindAsync(invoiceId);
        if (invoice == null)
        {
            return null;
        }

        _mapper.Map(updateDto, invoice);
        await _context.SaveChangesAsync();

        return _mapper.Map<InvoiceDto>(invoice);
    }

    public async Task<List<InvoiceDto>> GetAllInvoicesAsync()
    {
        var invoices = await _context.Invoices
            .Include(i => i.Shop)
            .ToListAsync();
        return _mapper.Map<List<InvoiceDto>>(invoices);
    }
    
    public async Task<InvoiceDto?> GetInvoiceByIdAsync(int invoiceId)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Shop)
            .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId);
        return invoice == null ? null : _mapper.Map<InvoiceDto>(invoice);
    }
}