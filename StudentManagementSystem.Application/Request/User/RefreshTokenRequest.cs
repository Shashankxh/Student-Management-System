namespace StudentManagementSystem.Application.Request.User;

public class RefreshTokenRequest
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}
