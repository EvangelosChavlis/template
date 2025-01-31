namespace server.src.Domain.Models.Geography;

/// <summary>
/// Represents a time zone with a standard identifier, offset, and description.
/// </summary>
public class TimeZone
{
    /// <summary>
    /// Gets or sets the unique identifier for the time zone.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the time zone (e.g., UTC, PST, EST).
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the UTC offset in hours (e.g., -8 for PST, 0 for UTC).
    /// </summary>
    public double UtcOffset { get; set; }

    /// <summary>
    /// Gets or sets whether the time zone follows daylight saving time (DST).
    /// </summary>
    public bool SupportsDaylightSaving { get; set; }

    /// <summary>
    /// Gets or sets the status indicating if the time zone is active.
    /// Determines whether the time zone is enabled for assignment or usage.
    /// </summary>
    public bool IsActive { get; set; }

    #region Navigation Properties
    /// <summary>
    /// Gets or sets the list of locations associated with this time zone.
    /// </summary>
    public List<Location> Locations { get; set; }
    #endregion
}