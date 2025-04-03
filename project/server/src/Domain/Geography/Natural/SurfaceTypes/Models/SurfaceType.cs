// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Domain.Geography.Natural.SurfaceTypes.Models;

/// <summary>
/// Represents a type of surface, such as coastal, mountainous, desert, or urban.
/// </summary>
public class SurfaceType : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the surface type (e.g., Mountainous, Coastal, Desert).
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a description of the surface characteristics.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets a unique code representing the surface type (e.g., "MNT" for Mountainous, "CST" for Coastal).
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the status indicating if the surface type is active.
    /// Determines whether the surface type is enabled for assignment or usage.
    /// </summary>
    public bool IsActive { get; set; }

    #region Navigation properties

    /// <summary>
    /// Gets or sets the list of locations associated with this surface type.
    /// </summary>
    public virtual List<Location> Locations { get; set; }
    
    #endregion
}