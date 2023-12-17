using Microsoft.AspNetCore.Mvc;
using RetailMaster.DTO;
using RetailMaster.Models;
using RetailMaster.Services;

namespace RetailMaster.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly UserService _userService;

    public AuthController(AuthService authService, UserService userService)
    {
        _authService = authService;
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var user = new User()
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                SecondName = registerDto.SecondName,
                Password = registerDto.Password,
                Role = registerDto.Role,
                ShopId = registerDto.ShopId
            };
            var userDto = await _authService.Register(user);
            return CreatedAtAction(nameof(Register), new { id = userDto.UserId }, userDto);
        }
        catch (Exception ex)
        {
            // You can return a more specific error message here
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var token = await _authService.Login(loginDto);
            if (token == null)
            {
                throw new Exception("Invalid login credentials");
            }

            // Assuming UserService is injected and available in AuthController
            var userDto = await _userService.GetUserByEmail(loginDto.Email);
            if (userDto == null)
            {
                throw new Exception("User not found");
            }

            return Ok(new { Token = token, User = userDto });
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }

}