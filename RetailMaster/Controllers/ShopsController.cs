using RetailMaster.DTO;
using RetailMaster.Models;

namespace RetailMaster.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

[ApiController]
[Route("[controller]")]
public class ShopsController : ControllerBase
{
    private readonly ShopService _shopService;

    public ShopsController(ShopService shopService)
    {
        _shopService = shopService;
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Add([FromBody] CreateShopDto createDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var shop = new Shop()
        {
            Name = createDto.Name,
            Address = createDto.Address,
            Phone = createDto.Phone
        };
        var shopDto = await _shopService.AddShop(shop);
        return CreatedAtAction(nameof(GetById), new { id = shopDto.ShopId }, shopDto);
    }

    [HttpGet("{shopId:int}")]
    public async Task<IActionResult> GetById(int shopId)
    {
        var shopDto = await _shopService.GetShopById(shopId);
        if (shopDto == null)
        {
            return NotFound($"Shop with ID {shopId} not found.");
        }
        return Ok(shopDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var shops = await _shopService.GetAllShops();
        return Ok(shops);
    }

    [HttpDelete("{shopId:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int shopId)
    {
        var success = await _shopService.DeleteShop(shopId);
        if (!success)
        {
            return NotFound($"Shop with ID {shopId} not found.");
        }
        return NoContent();
    }
}
