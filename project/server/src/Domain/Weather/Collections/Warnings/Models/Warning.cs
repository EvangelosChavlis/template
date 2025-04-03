// source
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.Forecasts.Models;

namespace server.src.Domain.Weather.Collections.Warnings.Models;

/// <summary>
/// Represents a weather warning, such as extreme weather alerts, related to specific forecasts.
/// This class is used to store warning data that can be associated with multiple forecasts.
/// </summary>
public class Warning : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the weather warning.
    /// This could be a title or brief description of the warning (e.g., "Severe Thunderstorm Warning").
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the weather warning.
    /// This provides additional details about the warning, such as the type of severe weather and its expected impact.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the code of the weather warning.
    /// This provides additional details about the warning, such as the type of severe weather and its expected impact.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the recommended actions for people in the affected area.
    /// Example: "Seek shelter immediately", "Avoid outdoor activities", etc.
    /// </summary>
    public string RecommendedActions { get; set; }

    /// <summary>
    /// Gets or sets the status indicating if the warning is active.
    /// Determines whether the warning is enabled for assignment or usage.
    /// </summary>
    public bool IsActive { get; set; }

    #region Navigation properties

    /// <summary>
    /// Gets or sets the list of forecasts associated with this warning.
    /// A single warning can be linked to multiple forecasts if the warning applies to more than one day.
    /// </summary>
    public virtual List<Forecast> Forecasts { get; set; }
    
    #endregion
}