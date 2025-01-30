// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Configurations.Auth;
using server.src.Persistence.Configurations.Metrics;
using server.src.Persistence.Configurations.Weather;

namespace server.Persistence.Configurations;

public static class DependencyInjection
{
    private readonly static string _authSchema = "auth";
    private readonly static string _weatherSchema = "weather";
    private readonly static string _metricsSchema = "metrics";

    public static void AddConfigurations(this ModelBuilder modelBuilder)
    {
        #region Auth Configuration
        modelBuilder.ApplyConfiguration(new UserConfiguration("Users", _authSchema));
        modelBuilder.ApplyConfiguration(new RoleConfiguration("Roles", _authSchema));
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration("UserRoles", _authSchema));
        modelBuilder.ApplyConfiguration(new UserLoginConfiguration("UserLogins", _authSchema));
        modelBuilder.ApplyConfiguration(new UserLogoutConfiguration("UserLogouts", _authSchema));
        modelBuilder.ApplyConfiguration(new UserClaimConfiguration("UserClaims", _authSchema));
        #endregion

        #region Weather Configuration
        modelBuilder.ApplyConfiguration(new ForecastConfiguration("Forecasts", _weatherSchema));
        modelBuilder.ApplyConfiguration(new WarningConfiguration("Warnings", _weatherSchema));
        #endregion

        #region Metrics Configuration
        modelBuilder.ApplyConfiguration(new AuditLogConfiguration("AuditLogs", _metricsSchema));
        modelBuilder.ApplyConfiguration(new TrailConfiguration("Trails", _metricsSchema));
        modelBuilder.ApplyConfiguration(new LogErrorConfiguration("LogErrors", _metricsSchema));
        modelBuilder.ApplyConfiguration(new TelemetryConfiguration("TelemetryRecords", _metricsSchema));
        modelBuilder.ApplyConfiguration(new StoryConfiguration("Stories", _metricsSchema));
        #endregion
    }
}