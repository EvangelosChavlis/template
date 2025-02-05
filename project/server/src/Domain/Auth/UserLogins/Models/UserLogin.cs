// source
using server.src.Domain.Auth.Users.Models;

namespace server.src.Domain.Auth.UserLogins.Models;

/// <summary>
/// Represents a user's login details, including the login provider and provider key.
/// </summary>
public class UserLogin
{
    /// <summary>
    /// Gets or sets the unique identifier for the user login entry.
    /// This can be used to uniquely identify an user login in system or database.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the login provider for the login (e.g. facebook, google)
    /// </summary>
    public string LoginProvider { get; set; }

    /// <summary>
    /// Gets or sets the unique provider identifier for this login.
    /// </summary>
    public string ProviderKey { get; set; }

    /// <summary>
    /// Gets or sets the friendly name used in a UI for this login.
    /// </summary>
    public string ProviderDisplayName { get; set; }

    /// <summary>
    /// The date and time of the login event.
    /// </summary>
    public DateTime Date { get; set; }

    #region Foreign keys
    /// <summary>
    /// Gets or sets the ID of the user associated with the user login.
    /// This establishes a relationship between the user login and a user.
    /// </summary>
    public Guid UserId { get; set; }
    #endregion

    #region Navigation properties
    /// <summary>
    /// Gets or sets the associated user for this login event.
    /// Represents the many-to-one relationship between the login record and the user.
    /// </summary>
    public virtual User User { get; set; }
    #endregion
}
