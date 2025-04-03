// source
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.Forecasts.Models;
using server.src.Domain.Weather.Collections.Observations.Models;

namespace server.src.Domain.Weather.Collections.MoonPhases.Models;

/// <summary>
/// Represents a specific moon phase with details such as illumination, duration, and significance.
/// </summary>
public class MoonPhase : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the moon phase.
    /// Example values: New Moon, Full Moon, Waxing Crescent, Waning Gibbous, etc.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a detailed description of the moon phase,
    /// including its impact on tides, visibility, and astronomical significance.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the code of the moon phase.
    /// Example values: New Moon, Full Moon, Waxing Crescent, Waning Gibbous, etc.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the percentage of the moon's surface that is illuminated during this phase.
    /// Value ranges from 0 to 100%.
    /// </summary>
    public double IlluminationPercentage { get; set; }

    /// <summary>
    /// Gets or sets the sequence order of this phase in the lunar cycle.
    /// Example: 1 for New Moon, 2 for Waxing Crescent, etc.
    /// </summary>
    public int PhaseOrder { get; set; }

    /// <summary>
    /// Gets or sets the duration of this moon phase in days.
    /// Represents the time span before transitioning to the next phase.
    /// </summary>
    public double DurationDays { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this phase is significant for astronomical or cultural events.
    /// </summary>
    public bool IsSignificant { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this moon phase is currently active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the date when this moon phase occurs.
    /// </summary>
    public DateTime OccurrenceDate { get; set; }

    #region Navigation properties

    /// <summary>
    /// Gets or sets the list of weather forecasts associated with this moon phase.
    /// </summary>
    public virtual List<Forecast> Forecasts { get; set; }

    /// <summary>
    /// Gets or sets the list of weather observations associated with this moon phase.
    /// </summary>
    public virtual List<Observation> Observations { get; set; }
    
    #endregion
}