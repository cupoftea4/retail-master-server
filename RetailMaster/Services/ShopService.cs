using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RetailMaster.Models;

namespace RetailMaster.Services;

public class ShopService
{
    private readonly RetailMasterContext _context;
    private readonly IMapper _mapper;

    public ShopService(RetailMasterContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Shop> AddShop(Shop shop)
    {
        _context.Shops.Add(shop);
        await _context.SaveChangesAsync();

        return shop;
    }

    public async Task<Shop?> GetShopById(int shopId)
    {
        var shop = await _context.Shops.FindAsync(shopId);
        return shop != null ? shop : null;
    }

    public async Task<List<Shop>> GetAllShops()
    {
        var shops = await _context.Shops.ToListAsync();
        return _mapper.Map<List<Shop>>(shops);
    }

    public async Task<bool> DeleteShop(int shopId)
    {
        var shop = await _context.Shops.FindAsync(shopId);
        if (shop == null)
        {
            return false;
        }

        _context.Shops.Remove(shop);
        await _context.SaveChangesAsync();
        return true;
    }
}
