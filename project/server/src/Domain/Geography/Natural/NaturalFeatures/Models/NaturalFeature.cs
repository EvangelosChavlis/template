// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Domain.Geography.Natural.NaturalFeatures.Models;

/// <summary>
/// Represents a geographical feature like a mountain, river, or island that can be associated with multiple locations.
/// </summary>
public class NaturalFeature : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the natural feature (e.g., Mountain, River, Island).
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a description of the natural feature (e.g., "The highest mountain in the world").
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the status indicating if the natural feature is active (enabled for assignment or usage).
    /// </summary>
    public bool IsActive { get; set; }

    #region Navigation properties

    /// <summary>
    /// Gets or sets the list of locations associated with this natural feature.
    /// A natural feature can span across many locations.
    /// </summary>
    public virtual List<Location> Locations { get; set; } = new List<Location>();

    #endregion
}