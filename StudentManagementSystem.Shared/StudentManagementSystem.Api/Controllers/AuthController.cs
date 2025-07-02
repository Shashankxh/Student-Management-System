using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Application.Request.User;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService, IValidator<LoginUser> validator) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IValidator<LoginUser> _validator = validator;

    [HttpPost("login")]
    public async Task<Result<UserResponce>> Login([FromBody] LoginUser request)
    {
        var validatorResponse = await _validator.ValidateAsync(request);
        if (!validatorResponse.IsValid) { throw new ValidationException(validatorResponse.Errors); }
        var result = await _authService.Login(request);
        return result;
    }
    [HttpPost("ResfreshToken")]
    public async Task<Result<UserResponce>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.RefreshToken(request);
        return result;
    }
    [HttpPost("LogOut")]
    public async Task<Result<string>> LogOut()
    {
        var result = await _authService.Logout();
        return result;
    }

    [HttpPost("RegisterUser")]
    public async Task<Result<UserResponce>> SignUp([FromQuery] string email, string password, string role)
    {
        return await _authService.RegisterUserAsync(email,password,role);
    }

}
