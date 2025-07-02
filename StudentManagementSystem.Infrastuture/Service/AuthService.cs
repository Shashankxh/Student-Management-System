using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Application.Request.User;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Domain.Entity;
using StudentManagementSystem.Shared.GlobalResponce;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StudentManagementSystem.Infrastuture.Service;

public class AuthService(IHttpContextAccessor contextAccessor, IConfiguration configuration, UserManager<User> userManager) : IAuthService
{
    private readonly IHttpContextAccessor? _contextAccessor = contextAccessor;
    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<User> _userManager = userManager;
    public async Task<Result<UserResponce>> Login(LoginUser request)
    {
        User? user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

        if (user == null) { return Result<UserResponce>.Failure("Incorect UserName or Password"); }

        bool validPassword = await _userManager.CheckPasswordAsync(user, request.Password!);
        if (!validPassword) { return Result<UserResponce>.Failure("Incorect UserName or Password"); }

        IList<string> roles = await _userManager.GetRolesAsync(user);
        string token = CreateAccessToken(user, roles);
        string refreshToken = CreateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);

        UserResponce? userResponce = new()
        {
            Token = token,
            RefreshToken = refreshToken,
        };
        return Result<UserResponce>.Success(userResponce);
    }
    public async Task<Result<UserResponce>> RegisterUserAsync(string email, string password, string role)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser != null)
        {
            return Result<UserResponce>.Failure("User already exists with this email.");
        }


        var user = new User
        {
            UserName = email,
            Email = email
        };

        var createResult = await _userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
        {
            var errorMsg = string.Join("; ", createResult.Errors.Select(e => e.Description));
            return Result<UserResponce>.Failure(errorMsg);
        }

        // Assign role
        var roleResult = await _userManager.AddToRoleAsync(user, role);
        if (!roleResult.Succeeded)
        {
            var errorMsg = string.Join("; ", roleResult.Errors.Select(e => e.Description));
            return Result<UserResponce>.Failure(errorMsg);
        }

        // Generate tokens
        var roles = await _userManager.GetRolesAsync(user);
        string token = CreateAccessToken(user, roles);
        string refreshToken = CreateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);

        var userResponce = new UserResponce
        {
            Token = token,
            RefreshToken = refreshToken
        };

        return Result<UserResponce>.Success(userResponce);
    }

    private static string CreateRefreshToken()
    {
        byte[]? randomNumber = new byte[32];
        using RandomNumberGenerator? rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private string CreateAccessToken(User user, IList<string> userRoles)
    {
        List<Claim>? claims =
        [
            new Claim(ClaimTypes.Email, user.Email!),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        ];

        foreach (string role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        SymmetricSecurityKey? key = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        SigningCredentials? creds = new(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken? token = new(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: creds
        );

        JwtSecurityTokenHandler? tokenHandler = new();

        string? tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }

    public async Task<Result<string>> Logout()
    {
        string? userEmail = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

        User? user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
        if (user == null) { return Result<string>.Failure("Something Went Wrong"); }

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;

        await _userManager.UpdateAsync(user);
        return Result<string>.Success("U are now Logged out");
    }

    public async Task<Result<UserResponce>> RefreshToken(RefreshTokenRequest request)
    {
        ClaimsPrincipal? principal = GetPrincipalFromToken(request.Token!);
        if (principal == null) { return null!; }

        string? userEmail = principal.FindFirst(ClaimTypes.Email)?.Value;
        User? user = await _userManager.FindByEmailAsync(userEmail!);

        if (user == null) { return null!; }

        if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return Result<UserResponce>.Failure("Failed To Varify the token");
        }

        string newAccessToken = CreateAccessToken(user, await _userManager.GetRolesAsync(user));
        string newRefreshToken = CreateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);


        UserResponce? userResponce = new()
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken,
        };
        return Result<UserResponce>.Success(userResponce);
    }

    private ClaimsPrincipal? GetPrincipalFromToken(string accesToken)
    {
        try
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
                ValidateIssuer = false,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = false,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true,
            };

            JwtSecurityTokenHandler tokenHandler = new();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(accesToken, tokenValidationParameters, out SecurityToken validatedToken);

            if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }
        catch
        {
            return null;
        }
    }
}
