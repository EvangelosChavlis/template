// packages
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// source
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Errors;
using server.src.Domain.Models.Metrics;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Configurations.Auth;
using server.src.Persistence.Configurations.Metrics;
using server.src.Persistence.Configurations.Weather;

namespace server.src.Persistence.Contexts;

public class DataContext : IdentityDbContext<User, Role, string, 
    IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, 
    IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    #region Weather
    public DbSet<Forecast> Forecasts { get; set; }
    public DbSet<Warning> Warnings { get; set; }
    #endregion

    #region Metrics 
    public DbSet<LogError> LogErrors { get; set; }
    public DbSet<Telemetry> TelemetryRecords { get; set; }
    #endregion

    
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region Auth Configuration
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        #endregion

        #region Weather Configuration
        modelBuilder.ApplyConfiguration(new ForecastConfiguration());
        modelBuilder.ApplyConfiguration(new WarningConfiguration());
        #endregion
       
        #region Metrics Configuration
        modelBuilder.ApplyConfiguration(new TelemetryConfiguration());
        modelBuilder.ApplyConfiguration(new LogErrorConfiguration());
        #endregion

        // Custom table names
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Role>().ToTable("Roles");
        modelBuilder.Entity<UserRole>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
    }
}