// source
using server.src.Domain.Common.Models;
using server.src.Domain.Support.Changes.Models;

namespace server.src.Domain.Support.ChangeTypes.Models;

/// <summary>
/// Represents a type of change in the system.
/// Defines different categories of modifications, such as additions, updates, or deletions.
/// </summary>
public class ChangeType : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the change type.
    /// Provides a label for the type of modification, such as "Update" or "Delete."
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the change type.
    /// Contains additional details about the purpose of this change type.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this change type is active.
    /// Determines whether the change type can be used in the system.
    /// </summary>
    public bool IsActive { get; set; }

    #region Navigation properties

    /// <summary>
    /// Gets or sets the list of changes associated with this change type.
    /// Establishes a relationship between changes and their respective types.
    /// </summary>
    public virtual List<Change> Changes { get; set; }
    
    #endregion
}