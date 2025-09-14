using SmsDemoApp.Domain.Entities.User;
using SmsDemoApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SmsDemoApp.Infrastructure.Identity.IdentitySetup.Stores;

internal class AppRoleStore(SmsDemoAppDbContext context, IdentityErrorDescriber? describer = null)
    : RoleStore<RoleEntity, SmsDemoAppDbContext, Guid>(context, describer);
