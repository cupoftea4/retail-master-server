using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RetailMaster.Models;

namespace RetailMaster.Services;

public class ProductCategoryService
{
    private readonly RetailMasterContext _context;
    private readonly IMapper _mapper;

    public ProductCategoryService(RetailMasterContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProductCategory>> GetAllProductCategoriesAsync()
    {
        return _mapper.Map<List<ProductCategory>>(await _context.ProductCategories.ToListAsync());
    }
}