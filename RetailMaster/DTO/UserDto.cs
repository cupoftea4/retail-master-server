using RetailMaster.Models;

namespace RetailMaster.DTO;

public class UserDto
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; } // Ensure this matches your UserRole enum
    public Shop? Shop { get; set; }
}

public class RegisterUserDto
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } // Ensure this matches your UserRole enum
    public int? ShopId { get; set; }
}

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
