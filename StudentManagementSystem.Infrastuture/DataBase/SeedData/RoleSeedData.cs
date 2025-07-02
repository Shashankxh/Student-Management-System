using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentManagementSystem.Infrastuture.DataBase.SeedData;

public class RoleSeedData : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(new IdentityRole
        {
            Id = "5e7ef962-9a66-46dd-9f58-f8d5e61a67d6",
            Name = "Admin",
            NormalizedName = "ADMIN",
            ConcurrencyStamp = "5e7ef962-9a66-46dd-9f58-f8d5e61a67d6"
        }, new IdentityRole
        {
            Id = "663333f9-7751-46d6-ada0-13ea7bdcc823",
            Name = "user",
            NormalizedName = "USER",
            ConcurrencyStamp = "663333f9-7751-46d6-ada0-13ea7bdcc823"
        });
    }
}
