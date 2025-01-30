// packages
using Microsoft.EntityFrameworkCore;
using server.Persistence.Configurations;


// source
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Errors;
using server.src.Domain.Models.Metrics;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Configurations.Auth;
using server.src.Persistence.Configurations.Metrics;
using server.src.Persistence.Configurations.Weather;
using server.src.Persistence.Indexes.Auth;
using server.src.Persistence.Indexes.Weather;

namespace server.src.Persistence.Contexts;

public class DataContext : DbContext
{
    #region Auth
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<UserLogout> UserLogouts { get; set; }
    public DbSet<UserClaim> UserClaims { get; set; }
    #endregion

    #region Weather
    public DbSet<Forecast> Forecasts { get; set; }
    public DbSet<Warning> Warnings { get; set; }
    #endregion

    #region Metrics 
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Trail> Trails { get; set; }
    public DbSet<LogError> LogErrors { get; set; }
    public DbSet<Telemetry> TelemetryRecords { get; set; }
    public DbSet<Story> Stories { get; set; }
    #endregion

    
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddConfigurations();

        // modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        // modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

        #region Auth Indexes
        modelBuilder.ApplyConfiguration(new UserIndexes());
        modelBuilder.ApplyConfiguration(new RoleIndexes());
        modelBuilder.ApplyConfiguration(new UserLoginIndexes());
        modelBuilder.ApplyConfiguration(new UserLogoutIndexes());
        modelBuilder.ApplyConfiguration(new UserRoleIndexes());
        #endregion

        #region Weather Indexes
        modelBuilder.ApplyConfiguration(new ForecastIndexes());
        modelBuilder.ApplyConfiguration(new WarningIndexes());
        #endregion

        #region Metrics Indexes
        #endregion
    }

}