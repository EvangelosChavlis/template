// source
using server.src.Domain.Support.Changes.Models;

namespace server.src.Domain.Support.ChangeLogs.Models;

/// <summary>
/// Represents a log entry that tracks changes made to the system.
/// </summary>
public class ChangeLog
{
    /// <summary>
    /// Gets or sets the unique identifier for the change log entry.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the version associated with the change log.
    /// This can be used to track changes across different application versions.
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Gets or sets the name of the change log entry.
    /// Provides a brief title or identifier for the logged change.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the change log entry.
    /// Contains details about what was modified in the system.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the change was logged.
    /// This helps in tracking when modifications were made.
    /// </summary>
    public DateTime DateTime { get; set; }

    #region Navigation properties

    /// <summary>
    /// Gets or sets the list of changes associated with this change log.
    /// Represents a collection of individual modifications recorded under this log.
    /// </summary>
    public virtual List<Change> Changes { get; set; }
    
    #endregion
}