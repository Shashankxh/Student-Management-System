using StudentManagementSystem.Application.Request.User;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Interface;

public interface IAuthService
{
    Task<Result<UserResponce>> Login(LoginUser request);
    Task<Result<UserResponce>> RefreshToken(RefreshTokenRequest request);
    Task<Result<string>> Logout();

    Task<Result<UserResponce>> RegisterUserAsync(string email, string password, string role);
}
