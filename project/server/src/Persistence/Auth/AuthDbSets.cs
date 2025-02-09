using Microsoft.EntityFrameworkCore;
using server.src.Domain.Auth.Roles.Models;
using server.src.Domain.Auth.UserClaims.Models;
using server.src.Domain.Auth.UserLogins.Models;
using server.src.Domain.Auth.UserLogouts.Models;
using server.src.Domain.Auth.UserRoles.Models;
using server.src.Domain.Auth.Users.Models;

namespace server.src.Persistence.Auth;

public class AuthDbSets
{
    public DbSet<User> Users { get; private set; }
    public DbSet<Role> Roles { get; private set; }
    public DbSet<UserRole> UserRoles { get; private set; }
    public DbSet<UserLogin> UserLogins { get; private set; }
    public DbSet<UserLogout> UserLogouts { get; private set; }
    public DbSet<UserClaim> UserClaims { get; private set; }

    public AuthDbSets(DbContext context)
    {
        Users = context.Set<User>();
        Roles = context.Set<Role>();
        UserRoles = context.Set<UserRole>();
        UserLogins = context.Set<UserLogin>();
        UserLogouts = context.Set<UserLogout>();
        UserClaims = context.Set<UserClaim>();
    }
}