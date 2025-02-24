// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;

namespace server.src.Domain.Geography.Administrative.Districts.Models;

/// <summary>
/// Represents a district within a municipality, including its name, population, 
/// and the neighborhoods it contains.
/// </summary>
public class District : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the district.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a brief description of the district.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the population of the district.
    /// </summary>
    public long Population { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the district is active.
    /// </summary>
    public bool IsActive { get; set; }

    #region Foreign keys

    /// <summary>
    /// Gets or sets the foreign key for the municipality that this district belongs to.
    /// </summary>
    public Guid MunicipalityId { get; set; }

    #endregion

    #region Navigation properties

    /// <summary>
    /// Gets or sets the associated municipality for the district.
    /// </summary>
    public virtual Municipality Municipality { get; set; }

    /// <summary>
    /// Gets or sets the list of neighborhoods within the district.
    /// </summary>
    public virtual List<Neighborhood> Neighborhoods { get; set; }

    #endregion
}