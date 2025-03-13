// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Domain.Geography.Administrative.Regions.Models;

namespace server.src.Domain.Geography.Administrative.Municipalities.Models;

/// <summary>
/// Represents a municipality, village, or small administrative unit within a region, including its population and status.
/// </summary>
public class Municipality : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the municipality or village.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a brief description of the municipality.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the population of the municipality.
    /// </summary>
    public long Population { get; set; }

    /// <summary>
    /// Gets or sets the area of the municipality in square kilometers.
    /// </summary>
    public double AreaKm2 { get; set; }

    /// <summary>
    /// Gets or sets the code of the municipality.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the municipality is active.
    /// </summary>
    public bool IsActive { get; set; }

    #region Foreign keys

    /// <summary>
    /// Gets or sets the foreign key for the region that this municipality belongs to.
    /// </summary>
    public Guid RegionId { get; set; }

    #endregion

    #region Navigation properties

    /// <summary>
    /// Gets or sets the associated region for the municipality.
    /// </summary>
    public virtual Region Region { get; set; }

    /// <summary>
    /// Gets or sets the list of districts in the municipality.
    /// </summary>
    public virtual List<District> Districts { get; set; }

    #endregion
}