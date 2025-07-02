using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Domain.Entity;
using StudentManagementSystem.Infrastuture.DataBase.SeedData;

namespace StudentManagementSystem.Infrastuture.DataBase;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<StudentDetails> StudentDetails { get; set; } = null!;
    public DbSet<StudentFeeReceipts> StudentFeeReceipts { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new IdentityRoleSeedData());
        modelBuilder.ApplyConfiguration(new RoleSeedData());
        modelBuilder.ApplyConfiguration(new UserSeedData());
        base.OnModelCreating(modelBuilder);
    }
}