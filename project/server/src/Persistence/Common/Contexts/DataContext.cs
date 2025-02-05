// // packages
// using Microsoft.EntityFrameworkCore;
// using server.src.Domain.Auth.Roles.Models;
// using server.src.Domain.Auth.UserClaims.Models;
// using server.src.Domain.Auth.UserLogins.Models;
// using server.src.Domain.Auth.UserLogouts.Models;
// using server.src.Domain.Auth.UserRoles.Models;
// using server.src.Domain.Auth.Users.Models;
// using server.src.Domain.Geography.ClimateZones.Models;
// using server.src.Domain.Geography.Locations.Models;
// using server.src.Domain.Geography.TerrainTypes.Models;
// using server.src.Domain.Geography.Timezones.Models;
// using server.src.Domain.Metrics.AuditLogs.Models;
// using server.src.Domain.Metrics.LogErrors.Models;
// using server.src.Domain.Metrics.Stories;
// using server.src.Domain.Metrics.TelemetryRecords.Models;
// using server.src.Domain.Metrics.Trails;
// using server.src.Domain.Weather.Forecasts.Models;
// using server.src.Domain.Weather.MoonPhases.Models;
// using server.src.Domain.Weather.Warnings.Models;
// using server.src.Persistence.Auth;
// using server.src.Persistence.Geography;



// // source
// using server.src.Persistence.Metrics;
// using server.src.Persistence.Weather;

// namespace server.src.Persistence.Contexts;

// public partial class DataContext : DbContext
// {
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
//     public DbSet<MoonPhase> MoonPhases { get; set; }
//     public DbSet<Warning> Warnings { get; set; }
//     #endregion

//     #region Metrics 
//     public DbSet<AuditLog> AuditLogs { get; set; }
//     public DbSet<Trail> Trails { get; set; }
//     public DbSet<LogError> LogErrors { get; set; }
//     public DbSet<TelemetryRecord> TelemetryRecords { get; set; }
//     public DbSet<Story> Stories { get; set; }
//     #endregion

//     #region  Geography
//     public DbSet<ClimateZone> ClimateZones { get; set; }
//     public DbSet<Location> Locations { get; set; }
//     public DbSet<TerrainType> TerrainTypes { get; set; }
//     public DbSet<Timezone> Timezones  { get; set; }
//     #endregion 

    
//     public DataContext(DbContextOptions<DataContext> options)
//         : base(options)
//     {
//     }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         base.OnModelCreating(modelBuilder);

//         modelBuilder.AddAuth();
//         modelBuilder.AddMetrics();
//         modelBuilder.AddGeography();
//         modelBuilder.AddWeather();
//     }

// }