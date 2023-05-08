namespace Ostral.Core.DTOs;

public class UserDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int PhoneNumber { get; set; }
    public string Email { get; set; } = string.Empty;
}