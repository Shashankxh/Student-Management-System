using Microsoft.AspNetCore.Identity;

namespace StudentManagementSystem.Domain.Entity;

public class User : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
