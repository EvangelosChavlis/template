// // packages
// using Microsoft.EntityFrameworkCore;

// // source
// namespace server.src.Persistence.Contexts;

// public class ArchiveContext : DbContext
// {
//     private readonly string _authSchema = "auth";
//     private readonly string _weatherSchema = "weather";
//     private readonly string _metricsSchema = "metrics";

//     #region Auth
//     public DbSet<User> Users { get; set; }
//     public DbSet<Role> Roles { get; set; }
//     public DbSet<UserRole> UserRoles { get; set; }
//     public DbSet<UserLogin> UserLogins { get; set; }
//     public DbSet<UserLogout> UserLogouts { get; set; }
//     public DbSet<UserClaim> UserClaims { get; set; }
//     #endregion

//     #region Weather
//     public DbSet<Forecast> Forecasts { get; set; }
//     public DbSet<Warning> Warnings { get; set; }
//     #endregion

//     #region Metrics 
//     public DbSet<AuditLog> AuditLogs { get; set; }
//     public DbSet<Trail> Trails { get; set; }
//     public DbSet<LogError> LogErrors { get; set; }
//     public DbSet<Telemetry> TelemetryRecords { get; set; }
//     public DbSet<Story> Stories { get; set; }
//     #endregion

    
//     public ArchiveContext(DbContextOptions<DataContext> options)
//         : base(options)
//     {
//     }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         base.OnModelCreating(modelBuilder);

//         // #region Auth Configuration
//         // modelBuilder.ApplyConfiguration(new UserConfiguration("Users", _authSchema));
//         // modelBuilder.ApplyConfiguration(new RoleConfiguration("Roles", _authSchema));
//         // modelBuilder.ApplyConfiguration(new UserRoleConfiguration("UserRoles", _authSchema));
//         // modelBuilder.ApplyConfiguration(new UserLoginConfiguration("UserLogins", _authSchema));
//         // modelBuilder.ApplyConfiguration(new UserLogoutConfiguration("UserLogouts", _authSchema));
//         // modelBuilder.ApplyConfiguration(new UserClaimConfiguration("UserClaims", _authSchema));
//         // #endregion

//         // #region Weather Configuration
//         // modelBuilder.ApplyConfiguration(new ForecastConfiguration("Forecasts", _weatherSchema));
//         // modelBuilder.ApplyConfiguration(new WarningConfiguration("Warnings", _weatherSchema));
//         // #endregion
       
//         // #region Metrics Configuration
//         // modelBuilder.ApplyConfiguration(new AuditLogConfiguration("AuditLogs", _metricsSchema));
//         // modelBuilder.ApplyConfiguration(new TrailConfiguration("Trails", _metricsSchema));
//         // modelBuilder.ApplyConfiguration(new LogErrorConfiguration("LogErrors", _metricsSchema));
//         // modelBuilder.ApplyConfiguration(new TelemetryConfiguration("TelemetryRecords", _metricsSchema));
//         // modelBuilder.ApplyConfiguration(new StoryConfiguration("Stories", _metricsSchema));
//         // #endregion

//         // modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
//         // modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
//     }
// }