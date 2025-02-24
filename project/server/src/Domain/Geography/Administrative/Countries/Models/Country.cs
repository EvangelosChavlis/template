// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Domain.Geography.Administrative.States.Models;

namespace server.src.Domain.Geography.Administrative.Countries.Models;

/// <summary>
/// Represents a country, including its name, ISO codes, capital, population, area, and its associated continent.
/// </summary>
public class Country : BaseEntity
{
    /// <summary>
    /// Gets or sets the official name of the country.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a brief description of the country.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the ISO 3166-1 alpha-2 country code (e.g., "US", "FR").
    /// </summary>
    public string IsoCode { get; set; }

    /// <summary>
    /// Gets or sets the official capital city of the country.
    /// </summary>
    public string Capital { get; set; }

    /// <summary>
    /// Gets or sets the population of the country.
    /// </summary>
    public long Population { get; set; }

    /// <summary>
    /// Gets or sets the area of the country in square kilometers.
    /// </summary>
    public double AreaKm2 { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the country is active.
    /// </summary>
    public bool IsActive { get; set; }

    #region Foreign keys

    /// <summary>
    /// Gets or sets the foreign key for the continent that this country belongs to.
    /// </summary>
    public Guid ContinentId { get; set; }

    #endregion

    #region Navigation properties

    /// <summary>
    /// Gets or sets the associated continent for the country.
    /// </summary>
    public virtual Continent Continent { get; set; }

    /// <summary>
    /// Gets or sets the list of states or provinces within the country.
    /// </summary>
    public virtual List<State> States { get; set; }

    #endregion
}