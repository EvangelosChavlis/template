namespace server.src.Domain.Models.Geography;

/// <summary>
/// Represents a type of terrain, such as coastal, mountainous, desert, or urban.
/// </summary>
public class TerrainType
{
    /// <summary>
    /// Gets or sets the unique identifier for the terrain type.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the terrain type (e.g., Mountainous, Coastal, Desert).
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a description of the terrain characteristics.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the status indicating if the terrain type is active.
    /// Determines whether the terrain type is enabled for assignment or usage.
    /// </summary>
    public bool IsActive { get; set; }

    #region Navigation Properties
    /// <summary>
    /// Gets or sets the list of locations associated with this terrain type.
    /// </summary>
    public List<Location> Locations { get; set; }
    #endregion
}