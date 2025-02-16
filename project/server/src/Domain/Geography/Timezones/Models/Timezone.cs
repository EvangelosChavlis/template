using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Locations.Models;

namespace server.src.Domain.Geography.Timezones.Models;

/// <summary>
/// Represents a time zone with a standard identifier, offset, and description.
/// </summary>
public class Timezone : BaseEntity
{
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

    /// <summary>
    /// Gets or sets the description of the time zone (e.g., "Pacific Standard Time (PST)").
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the UTC offset during daylight saving time (if applicable).
    /// If SupportsDaylightSaving is false, this will be the same as UtcOffset.
    /// </summary>
    public double? DstOffset { get; set; }

    #region Navigation properties

    /// <summary>
    /// Gets or sets the list of locations associated with this time zone.
    /// </summary>
    public virtual List<Location> Locations { get; set; }

    #endregion
}
