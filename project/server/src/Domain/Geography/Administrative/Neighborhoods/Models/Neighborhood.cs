// source
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Domain.Geography.Administrative.Stations.Models;

namespace server.src.Domain.Geography.Administrative.Neighborhoods.Models;

/// <summary>
/// Represents a neighborhood within a district, including its name, population, and the locations it contains.
/// </summary>
public class Neighborhood : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the neighborhood.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a brief description of the neighborhood.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the population of the neighborhood.
    /// </summary>
    public long Population { get; set; }

    /// <summary>
    /// Gets or sets the area of the neighborhood.
    /// </summary>
    public double AreaKm2 { get; set; }

    /// <summary>
    /// Gets or sets the ZIP code of the neighborhood, 
    /// which helps in identifying postal areas.
    /// </summary>
    public string Zipcode { get; set; }

    /// <summary>
    /// Gets or sets the area of the neighborhood.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the neighborhood is active.
    /// </summary>
    public bool IsActive { get; set; }

    #region Foreign keys

    /// <summary>
    /// Gets or sets the foreign key for the district that this neighborhood belongs to.
    /// </summary>
    public Guid DistrictId { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the station that this neighborhood belongs to.
    /// </summary>
    public Guid? StationId { get; set; }

    #endregion

    #region Navigation properties

    /// <summary>
    /// Gets or sets the associated district for the neighborhood.
    /// </summary>
    public virtual District District { get; set; }

    /// <summary>
    /// Gets or sets the associated weather station for the neighborhood.
    /// </summary>
    public virtual Station? Station { get; set; }

    /// <summary>
    /// Gets or sets the list of users associated with this neighborhood.
    /// Represents a one-to-many relationship between the neighborhood and users.
    /// </summary>
    public virtual List<User> Users { get; set; }

    #endregion
}
