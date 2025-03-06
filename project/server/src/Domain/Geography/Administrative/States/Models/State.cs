// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Domain.Geography.Administrative.Regions.Models;

namespace server.src.Domain.Geography.Administrative.States.Models;

/// <summary>
/// Represents a state, province, or administrative region within a country, including its name, capital, population, and area.
/// </summary>
public class State : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the state or province.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a brief description of the state.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the official capital city of the state.
    /// </summary>
    public string Capital { get; set; }

    /// <summary>
    /// Gets or sets the population of the state.
    /// </summary>
    public long Population { get; set; }

    /// <summary>
    /// Gets or sets the area of the state in square kilometers.
    /// </summary>
    public double AreaKm2 { get; set; }

    /// <summary>
    /// Gets or sets the code of the state.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the state is active.
    /// </summary>
    public bool IsActive { get; set; }

    #region Foreign keys

    /// <summary>
    /// Gets or sets the foreign key for the country that this state belongs to.
    /// </summary>
    public Guid CountryId { get; set; }

    #endregion

    #region Navigation properties

    /// <summary>
    /// Gets or sets the associated country for the state.
    /// </summary>
    public virtual Country Country { get; set; }

    /// <summary>
    /// Gets or sets the list of regions within the state.
    /// </summary>
    public virtual List<Region> Regions { get; set; }

    #endregion
}
