using Backend_Area42_3.Enums;

namespace Backend_Area42_3.Models;

public class User
{
    public int Id { get; set; }
    public string Phone { get; set; } = "";
    public required string Name { get; set; }
    public required UserRole Role { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
}
