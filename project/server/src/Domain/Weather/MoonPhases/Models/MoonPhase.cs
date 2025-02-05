// source
using server.src.Domain.Weather.Forecasts.Models;

namespace server.src.Domain.Weather.MoonPhases.Models;

/// <summary>
/// Represents a moon phase type with additional details.
/// </summary>
public class MoonPhase
{
    /// <summary>
    /// Gets or sets the unique identifier for the moon phase.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the moon phase.
    /// Example values: New Moon, Full Moon, Waxing Crescent, Waning Gibbous, etc.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a description of the moon phase.
    /// Provides additional details about the phase, such as its impact on tides and visibility.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the illumination percentage of the moon during this phase.
    /// This value represents how much of the moon is visible from Earth (0 to 100%).
    /// </summary>
    public double IlluminationPercentage { get; set; }

    /// <summary>
    /// Gets or sets the phase order in the lunar cycle.
    /// Example: 1 for New Moon, 2 for Waxing Crescent, etc.
    /// </summary>
    public int PhaseOrder { get; set; }

    /// <summary>
    /// Gets or sets the duration of this moon phase in days.
    /// Represents how long this particular phase lasts before transitioning.
    /// </summary>
    public double DurationDays { get; set; }

    /// <summary>
    /// Gets or sets whether this phase is considered significant for astronomical events.
    /// </summary>
    public bool IsSignificant { get; set; }

    /// <summary>
    /// Gets or sets the date when this moon phase occurs.
    /// </summary>
    public DateTime OccurrenceDate { get; set; }

    #region Navigation properties

    /// <summary>
    /// Gets or sets the list of forecasts that reference this moon phase.
    /// </summary>
    public virtual List<Forecast> Forecasts { get; set; }
    
    #endregion
}