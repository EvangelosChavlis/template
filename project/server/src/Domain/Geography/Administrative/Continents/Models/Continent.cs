// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Countries.Models;

namespace server.src.Domain.Geography.Administrative.Continents.Models;

/// <summary>
/// Represents a continent, such as Africa, Asia, or Europe, including basic information and the countries it encompasses.
/// </summary>
public class Continent : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the continent (e.g., Africa, Asia, Europe).
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the code for the continent (e.g., "AF" for Africa, "AS" for Asia).
    /// The code is typically two uppercase letters representing the continent's region.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets a brief description of the continent.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the total population of the continent.
    /// </summary>
    public long Population { get; set; }

    /// <summary>
    /// Gets or sets the total area of the continent in square kilometers.
    /// </summary>
    public double AreaKm2 { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the continent is active.
    /// </summary>
    public bool IsActive { get; set; }

    #region Navigation properties

    /// <summary>
    /// Gets or sets the list of countries within the continent.
    /// </summary>
    public virtual List<Country> Countries { get; set; }

    #endregion
}