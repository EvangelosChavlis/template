// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Domain.Geography.Administrative.States.Models;

namespace server.src.Domain.Geography.Administrative.Regions.Models;

/// <summary>
/// Represents a region within a state, including its name, area, and the municipalities it contains.
/// </summary>
public class Region : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the region.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a brief description of the region.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the area of the region in square kilometers.
    /// </summary>
    public double AreaKm2 { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the region is active.
    /// </summary>
    public bool IsActive { get; set; }

    #region Foreign keys

    /// <summary>
    /// Gets or sets the foreign key for the state that this region belongs to.
    /// </summary>
    public Guid StateId { get; set; }

    #endregion

    #region Navigation properties

    /// <summary>
    /// Gets or sets the associated state for the region.
    /// </summary>
    public virtual State State { get; set; }

    /// <summary>
    /// Gets or sets the list of municipalities in the region.
    /// </summary>
    public virtual List<Municipality> Municipalities { get; set; }

    #endregion
}