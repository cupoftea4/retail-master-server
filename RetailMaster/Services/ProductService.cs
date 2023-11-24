using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using RetailMaster.DTO;
using RetailMaster.Middleware;
using RetailMaster.Models;

namespace RetailMaster.Services;

public class ProductService
{
    private readonly RetailMasterContext _context;
    private readonly IMapper _mapper;

    public ProductService(RetailMasterContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        return _mapper.Map<List<ProductDto>>(await _context.Products.ToListAsync());
    }
    
    public async Task<ProductDto> AddProductAsync(Product product)
    {
        try
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        } catch (DbUpdateException ex) when (ex.InnerException is MySqlException { Number: 1062 })
        {
            throw new DuplicateEntryException("Duplicate barcode entry.", ex);
        } catch (DbUpdateException ex) when (ex.InnerException is MySqlException { Number: 1452 })
        {
            throw new ForeignKeyConstraintException(
                "Invalid Category ID or other foreign key constraint violation.", ex);
        }
    }
    
    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto updateDto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return null;
        }

        // Update name if provided
        if (!string.IsNullOrWhiteSpace(updateDto.Name))
        {
            product.Name = updateDto.Name;
        }

        // Update category if provided
        if (updateDto.CategoryId.HasValue)
        {
            product.CategoryId = updateDto.CategoryId.Value;
        }

        await _context.SaveChangesAsync();
        return _mapper.Map<ProductDto>(product);
    }
    
}
