using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentManagementSystem.Infrastuture.DataBase.SeedData;

public class IdentityRoleSeedData : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(new IdentityUserRole<string>
        {
            RoleId = "5e7ef962-9a66-46dd-9f58-f8d5e61a67d6",
            UserId = "7ca97345-73c1-46be-9b3f-c8dd448d1286"
        }, new IdentityUserRole<string>
        {
            RoleId = "663333f9-7751-46d6-ada0-13ea7bdcc823",
            UserId = "6deda837-782d-40c1-84bf-3e1ac1cf6d1e"
        });
    }
}
