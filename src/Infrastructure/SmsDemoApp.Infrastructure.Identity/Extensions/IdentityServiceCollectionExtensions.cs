using SmsDemoApp.Application.Contracts.User;
using SmsDemoApp.Domain.Entities.User;
using SmsDemoApp.Infrastructure.Identity.IdentitySetup.Factories;
using SmsDemoApp.Infrastructure.Identity.IdentitySetup.Stores;
using SmsDemoApp.Infrastructure.Identity.Services.Implementations;
using SmsDemoApp.Infrastructure.Identity.Services.Models;
using SmsDemoApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SmsDemoApp.Infrastructure.Identity.Extensions;

public static class IdentityServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserClaimsPrincipalFactory<UserEntity>, AppUserClaimPrincipalFactory>();
        services.AddScoped<IRoleStore<RoleEntity>, AppRoleStore>();
        services.AddScoped<IUserStore<UserEntity>, AppUserStore>();

        services.AddIdentity<UserEntity, RoleEntity>(options =>
            {
                options.Stores.ProtectPersonalData = false;

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;
                options.User.RequireUniqueEmail = false;
            }).AddRoleStore<AppRoleStore>()
            .AddUserStore<AppUserStore>()
            .AddClaimsPrincipalFactory<AppUserClaimPrincipalFactory>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<SmsDemoAppDbContext>();

        services.Configure<JwtConfiguration>(configuration.GetSection(nameof(JwtConfiguration)));

        services.AddScoped<IJwtService, JwtServiceImplementation>();
        services.AddScoped<IUserManager, UserManagerImplementation>();
        
        return services;
    }
}