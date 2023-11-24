using RetailMaster.Services;

namespace RetailMaster.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetById(int userId)
    {
        var userDto = await _userService.GetUserById(userId);
        if (userDto == null)
        {
            return NotFound($"User with ID {userId} not found.");
        }
        return Ok(userDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpDelete("{userId:int}")]
    [Authorize(Roles = "admin")] // Assuming only admins can delete users
    public async Task<IActionResult> Delete(int userId)
    {
        var success = await _userService.DeleteUser(userId);
        if (!success)
        {
            return NotFound($"User with ID {userId} not found.");
        }
        return NoContent();
    }
}
