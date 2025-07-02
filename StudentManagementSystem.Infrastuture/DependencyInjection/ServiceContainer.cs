using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using StudentManagementSystem.Application.Behavior;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Application.Mapper;
using StudentManagementSystem.Application.Validation.User;
using StudentManagementSystem.Domain.Entity;
using StudentManagementSystem.Infrastuture.DataBase;
using StudentManagementSystem.Infrastuture.Middleware;
using StudentManagementSystem.Infrastuture.Repository;
using StudentManagementSystem.Infrastuture.Service;
using System.Text;

namespace StudentManagementSystem.Infrastuture.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IUnitOfWork, UnitofWork>();
        services.AddTransient<IAuthService, AuthService>();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("AppConnectionString"));
            options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        });

        services.AddIdentity<User, IdentityRole>()
                       .AddEntityFrameworkStores<AppDbContext>()
                       .AddDefaultTokenProviders();


        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });


            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer("Bearer", options =>
             {

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = configuration.GetSection("Jwt:Issuer").Value!,
                     ValidAudience = configuration.GetSection("Jwt:Audience").Value!,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value!)),
                     ClockSkew = TimeSpan.FromMinutes(15),
                 };

             });

        services.AddValidatorsFromAssemblyContaining<Application.Validation.StudentDetailValidation.AddValidation>();
        services.AddValidatorsFromAssemblyContaining<Application.Validation.StudentDetailValidation.UpdateValidation>();
        services.AddValidatorsFromAssemblyContaining<Application.Validation.StudentFeeReceiptValidation.AddValidation>();
        services.AddValidatorsFromAssemblyContaining<Application.Validation.StudentFeeReceiptValidation.UpdateValidation>();
        services.AddValidatorsFromAssemblyContaining<AuthUserValidation>();

        services.AddIdentityCore<User>()
       .AddRoles<IdentityRole>()
       .AddTokenProvider<DataProtectorTokenProvider<User>>("StudentManagementSystem")
       .AddEntityFrameworkStores<AppDbContext>()
       .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = true;
            o.Password.RequiredLength = 6;
            o.Password.RequiredUniqueChars = 1;
            o.Password.RequireUppercase = true;
        });

        services.AddAutoMapper(typeof(AutoMapperProfile));

        Log.Logger = new LoggerConfiguration()
         .MinimumLevel.Information()
         .WriteTo.Debug()
         .WriteTo.Console()
         .CreateLogger();

        services.AddTransient<GlobalExceptionHandler>();

        return services;
    }
    public static IApplicationBuilder UseInfrastructurePolicies(this IApplicationBuilder app)

    {
        var imagesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
        if (!Directory.Exists(imagesDirectory))
        {
            Directory.CreateDirectory(imagesDirectory);
        }
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images")),
            RequestPath = "/Images"
        });

        //app.UseExceptionHandler();

        app.UseMiddleware<GlobalExceptionHandler>();
        return app;
    }
}
