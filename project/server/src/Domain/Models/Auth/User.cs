// source
using server.src.Domain.Models.Metrics;

namespace server.src.Domain.Models.Auth;

/// <summary>
/// Represents a user in the authentication system, including properties for authentication, contact, and profile information.
/// </summary>
public class User
{
    #region Authentication Information

    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the normalized username used for authentication.
    /// </summary>
    public string NormalizedUserName { get; set; }

    /// <summary>
    /// Gets or sets the username used for authentication.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the normalized email address used for authentication.
    /// </summary>
    public string NormalizedEmail { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the email address has been confirmed.
    /// </summary>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Gets or sets the hashed password of the user.
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether two-factor authentication is enabled.
    /// </summary>
    public bool TwoFactorEnabled { get; set; }

    /// <summary>
    /// Gets or sets the phone number associated with the user.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the phone number has been confirmed.
    /// </summary>
    public bool PhoneNumberConfirmed { get; set; }

    /// <summary>
    /// Gets or sets the number of failed login attempts.
    /// </summary>
    public int AccessFailedCount { get; set; }

    /// <summary>
    /// Gets or sets the concurrency stamp used to handle concurrent updates.
    /// </summary>
    public string ConcurrencyStamp { get; set; }

    /// <summary>
    /// Gets or sets the security stamp used to verify the identity of the user.
    /// </summary>
    public string SecurityStamp { get; set; }

    /// <summary>
    /// Gets or sets the lockout end date for the user, if applicable.
    /// </summary>
    public DateTimeOffset? LockoutEnd { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is locked out.
    /// </summary>
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// Gets or sets the password reset token for the user.
    /// Used for securely resetting the password.
    /// </summary>
    public string PasswordResetToken { get; set; }

    /// <summary>
    /// Gets or sets the expiry time for the password reset token.
    /// Ensures that the token is valid only for a certain period.
    /// </summary>
    public DateTime? PasswordResetTokenExpiry { get; set; }

    /// <summary>
    /// Gets or sets the two-factor authentication token for the user.
    /// Used for 2FA validation.
    /// </summary>
    public string TwoFactorToken { get; set; }

    /// <summary>
    /// Gets or sets the expiry time for the two-factor authentication token.
    /// Ensures that the token is valid only for a short time.
    /// </summary>
    public DateTime? TwoFactorTokenExpiry { get; set; }

    #endregion

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
    public string DateOfBirth { get; set; }

    #endregion

    #region System Information

    /// <summary>
    /// Gets or sets a value indicating whether the user is active in the system.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the version GUID for optimistic concurrency control.
    /// </summary>
    public Guid Version { get; set; }
    #endregion

    #region Navigation Properties

    /// <summary>
    /// Gets or sets the list of user roles associated with the user.
    /// Represents the many-to-many relationship between users and roles.
    /// </summary>
    public List<UserRole> UserRoles { get; set; }

    /// <summary>
    /// Gets or sets the list of telemetry records associated with the user.
    /// Represents a one-to-many relationship between the user and telemetry data.
    /// </summary>
    public List<Telemetry> TelemetryRecords { get; set; }

    /// <summary>
    /// Gets or sets the list of user login records associated with the user.
    /// Represents a one-to-many relationship between the user and their login events.
    /// </summary>
    public List<UserLogin> UserLogins { get; set; }

    /// <summary>
    /// Gets or sets the list of user login records associated with the user.
    /// Represents a one-to-many relationship between the user and their logout events.
    /// </summary>
    public List<UserLogout> UserLogouts { get; set; }

    /// <summary>
    /// Gets or sets the list of user claim records associated with the user.
    /// Represents a one-to-many relationship between the user and their claim events.
    /// </summary>
    public List<UserClaim> UserClaims { get; set; }

    /// <summary>
    /// Gets or sets the list of audit logs associated with the user.
    /// Represents a one-to-many relationship between the user and their audit logs.
    /// </summary>
    public List<AuditLog> AuditLogs { get; set; }

    /// <summary>
    /// Gets or sets the list of historical records associated with the user.  
    /// Represents a one-to-many relationship between the user and their activity history,  
    /// tracking changes and interactions over time.  
    /// </summary>
    public List<Story> Stories { get; set; }

    #endregion
}
