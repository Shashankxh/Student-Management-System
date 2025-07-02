using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystem.Domain.Entity;

namespace StudentManagementSystem.Infrastuture.DataBase.SeedData;

public class UserSeedData : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var hasher = new PasswordHasher<User>();
        builder.HasData(new User
        {
            Id = "7ca97345-73c1-46be-9b3f-c8dd448d1286",
            UserName = "Admin",
            Email = "admin@yopmail.com",
            NormalizedEmail = "admin@yopmail.com".ToUpper(),
            PasswordHash = hasher.HashPassword(null!, "Admin@123"),
            NormalizedUserName = "Admin".ToUpper(),
            EmailConfirmed = true,
            RefreshToken = null,
            RefreshTokenExpiryTime = null
        },
        new User
        {
            Id = "6deda837-782d-40c1-84bf-3e1ac1cf6d1e",
            UserName = "User",
            Email = "user@yopmail.com",
            NormalizedEmail = "user@yopmail.com".ToUpper(),
            PasswordHash = hasher.HashPassword(null!, "User@123"),
            NormalizedUserName = "User".ToUpper(),
            EmailConfirmed = true,
            RefreshToken = null,
            RefreshTokenExpiryTime = null
        });
    }
}
