// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Weather.Tools.HealthStatuses;
using server.src.Persistence.Weather.Tools.Sensors;
using server.src.Persistence.Weather.Tools.Units;

namespace server.src.Persistence.Weather.Tools;

public static class SetupBuilder
{
    private readonly static string _toolsSchema = "weather_tools";

    public static void SetupTools(this ModelBuilder modelBuilder)
    {
        #region Configuration
        modelBuilder.ApplyConfiguration(new HealthStatusConfiguration("HealthStasus", _toolsSchema));
        modelBuilder.ApplyConfiguration(new SensorConfiguration("Sensors", _toolsSchema));
        modelBuilder.ApplyConfiguration(new UnitConfiguration("Units", _toolsSchema));
        #endregion

        #region Indexes
        modelBuilder.ApplyConfiguration(new HealthStatusIndexes());
        modelBuilder.ApplyConfiguration(new SensorIndexes());
        modelBuilder.ApplyConfiguration(new UnitIndexes());
        #endregion
    }
}