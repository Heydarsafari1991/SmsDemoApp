using SmsDemoApp.Domain.Entities.User;
using SmsDemoApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SmsDemoApp.Infrastructure.Identity.IdentitySetup.Stores;

internal class AppUserStore(SmsDemoAppDbContext context, IdentityErrorDescriber? describer = null)
    : UserStore<UserEntity, RoleEntity, SmsDemoAppDbContext, Guid, UserClaimEntity, UserRoleEntity, UserLoginEntity,
        UserTokenEntity,
        RoleClaimEntity>(context, describer);