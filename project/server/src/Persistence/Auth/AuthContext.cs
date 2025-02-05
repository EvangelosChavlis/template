// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Domain.Auth.Roles.Models;
using server.src.Domain.Auth.UserClaims.Models;
using server.src.Domain.Auth.UserLogins.Models;
using server.src.Domain.Auth.UserLogouts.Models;
using server.src.Domain.Auth.UserRoles.Models;
using server.src.Domain.Auth.Users.Models;

namespace server.src.Persistence.Auth;

public class AuthContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<UserLogout> UserLogouts { get; set; }
    public DbSet<UserClaim> UserClaims { get; set; }

    public AuthContext(DbContextOptions<AuthContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddAuth();
    }
}