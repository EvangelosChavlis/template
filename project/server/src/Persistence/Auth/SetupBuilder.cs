// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Auth.Roles;
using server.src.Persistence.Auth.UserClaims;
using server.src.Persistence.Auth.UserLogins;
using server.src.Persistence.Auth.UserLogouts;
using server.src.Persistence.Auth.UserRoles;
using server.src.Persistence.Auth.Users;

namespace server.src.Persistence.Auth;

public static class SetupBuilder
{
    private readonly static string _authSchema = "auth";

    public static void SetupAuth(this ModelBuilder modelBuilder)
    {
        #region Configuration
        modelBuilder.ApplyConfiguration(new UserConfiguration("Users", _authSchema));
        modelBuilder.ApplyConfiguration(new RoleConfiguration("Roles", _authSchema));
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration("UserRoles", _authSchema));
        modelBuilder.ApplyConfiguration(new UserLoginConfiguration("UserLogins", _authSchema));
        modelBuilder.ApplyConfiguration(new UserLogoutConfiguration("UserLogouts", _authSchema));
        modelBuilder.ApplyConfiguration(new UserClaimConfiguration("UserClaims", _authSchema));
        #endregion

        #region Indexes
        modelBuilder.ApplyConfiguration(new RoleIndexes());
        modelBuilder.ApplyConfiguration(new UserLoginIndexes());
        modelBuilder.ApplyConfiguration(new UserLogoutIndexes());
        modelBuilder.ApplyConfiguration(new UserRoleIndexes());
        modelBuilder.ApplyConfiguration(new UserIndexes());
        #endregion
    }
}

