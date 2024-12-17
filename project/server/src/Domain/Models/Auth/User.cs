// packages
using Microsoft.AspNetCore.Identity;

namespace server.src.Domain.Models.Auth;

/// <summary>
/// Represents a user in the authentication system, extending the IdentityUser class to include additional properties.
/// </summary>
public class User : IdentityUser
{
    #region Personal Information

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the initial password of the user.
    /// This is used for storing the initial password setup before any user can log in.
    /// </summary>
    public string InitialPassword { get; set; }

    #endregion

    #region Contact Information

    /// <summary>
    /// Gets or sets the user's address.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the user's postal code.
    /// </summary>
    public string ZipCode { get; set; }

    /// <summary>
    /// Gets or sets the user's city.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Gets or sets the user's state.
    /// </summary>
    public string State { get; set; }

    /// <summary>
    /// Gets or sets the user's country.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Gets or sets the user's mobile phone number.
    /// </summary>
    public string MobilePhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the mobile phone number has been confirmed.
    /// </summary>
    public bool MobilePhoneNumberConfirmed { get; set; }

    #endregion

    #region Profile Information

    /// <summary>
    /// Gets or sets the user's bio or short description about themselves.
    /// </summary>
    public string Bio { get; set; }

    /// <summary>
    /// Gets or sets the user's date of birth.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    #endregion

    #region System Information

    /// <summary>
    /// Gets or sets a value indicating whether the user is active in the system.
    /// </summary>
    public bool IsActive { get; set; }

    #endregion

    #region Navigation Properties

    /// <summary>
    /// Gets or sets the list of user roles associated with the user.
    /// Represents the many-to-many relationship between users and roles.
    /// </summary>
    public List<UserRole> UserRoles { get; set; }

    #endregion
}
