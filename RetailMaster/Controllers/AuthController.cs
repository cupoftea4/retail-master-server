using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailMaster.DTO;
using RetailMaster.Models;
using RetailMaster.Services;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [Authorize(Roles = "admin")]
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
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            // You can return a more specific error message here
            return Unauthorized(ex.Message);
        }
    }
}