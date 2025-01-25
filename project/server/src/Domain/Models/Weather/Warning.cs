namespace server.src.Domain.Models.Weather;

/// <summary>
/// Represents a weather warning, such as extreme weather alerts, related to specific forecasts.
/// This class is used to store warning data that can be associated with multiple forecasts.
/// </summary>
public class Warning
{
    /// <summary>
    /// Gets or sets the unique identifier for the warning.
    /// This is used to uniquely identify the warning in the system.
    /// </summary>
    public Guid Id { get; set; }

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
    /// Gets or sets the version GUID for optimistic concurrency control.
    /// </summary>
    public Guid Version { get; set; }

    #region Navigation Properties
    /// <summary>
    /// Gets or sets the list of forecasts associated with this warning.
    /// A single warning can be linked to multiple forecasts if the warning applies to more than one day.
    /// </summary>
    public List<Forecast> Forecasts { get; set; }
    #endregion
}