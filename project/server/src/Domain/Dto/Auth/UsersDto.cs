namespace server.src.Domain.Dto.Auth;

/// <summary>
/// Represents a summarized user record for listing purposes.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
/// <param name="FirstName">The user's first name.</param>
/// <param name="LastName">The user's last name.</param>
/// <param name="Email">The user's email address.</param>
/// <param name="UserName">The user's username.</param>
/// <param name="PhoneNumber">The user's phone number.</param>
/// <param name="MobilePhoneNumber">The user's mobile phone number.</param>
public record ListItemUserDto(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string PhoneNumber,
    string MobilePhoneNumber
);

/// <summary>
/// Represents detailed information about a user.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
/// <param name="FirstName">The user's first name.</param>
/// <param name="LastName">The user's last name.</param>
/// <param name="Email">The user's email address.</param>
/// <param name="EmailConfirmed">Indicates whether the email is confirmed.</param>
/// <param name="UserName">The user's username.</param>
/// <param name="LockoutEnabled">Indicates whether lockout is enabled for the user.</param>
/// <param name="LockoutEnd">The end date of the lockout period, if applicable.</param>
/// <param name="InitialPassword">The user's initial password (if applicable).</param>
/// <param name="Address">The user's physical address.</param>
/// <param name="ZipCode">The user's ZIP code.</param>
/// <param name="City">The city of the user's address.</param>
/// <param name="State">The state of the user's address.</param>
/// <param name="Country">The country of the user's address.</param>
/// <param name="PhoneNumber">The user's phone number.</param>
/// <param name="PhoneNumberConfirmed">Indicates whether the phone number is confirmed.</param>
/// <param name="MobilePhoneNumber">The user's mobile phone number.</param>
/// <param name="MobilePhoneNumberConfirmed">Indicates whether the mobile phone number is confirmed.</param>
/// <param name="Bio">A brief biography of the user.</param>
/// <param name="DateOfBirth">The user's date of birth.</param>
/// <param name="IsActive">Indicates whether the user is active.</param>
/// <param name="Roles">The list of roles assigned to the user.</param>
public record ItemUserDto(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    bool EmailConfirmed,
    string UserName,
    bool LockoutEnabled,
    string LockoutEnd,
    string InitialPassword,
    string Address,
    string ZipCode,
    string City,
    string State,
    string Country,
    string PhoneNumber,
    bool PhoneNumberConfirmed,
    string MobilePhoneNumber,
    bool MobilePhoneNumberConfirmed,
    string Bio,
    string DateOfBirth,
    bool IsActive,
    List<string> Roles
);

/// <summary>
/// Represents the data required to create or update a user.
/// </summary>
/// <param name="FirstName">The user's first name.</param>
/// <param name="LastName">The user's last name.</param>
/// <param name="Email">The user's email address.</param>
/// <param name="UserName">The user's username.</param>
/// <param name="Password">The user's password.</param>
/// <param name="Address">The user's physical address.</param>
/// <param name="ZipCode">The ZIP code of the user's address.</param>
/// <param name="City">The city of the user's address.</param>
/// <param name="State">The state of the user's address.</param>
/// <param name="Country">The country of the user's address.</param>
/// <param name="PhoneNumber">The user's phone number.</param>
/// <param name="MobilePhoneNumber">The user's mobile phone number.</param>
/// <param name="Bio">A brief biography of the user.</param>
/// <param name="DateOfBirth">The user's date of birth.</param>
public record UserDto(
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password,
    string Address,
    string ZipCode,
    string City,
    string State,
    string Country,
    string PhoneNumber,
    string MobilePhoneNumber,
    string Bio,
    DateTime DateOfBirth
);

/// <summary>
/// Represents the credentials required for a user login request.
/// </summary>
/// <param name="Username">The username of the user.</param>
/// <param name="Password">The password of the user.</param>
public record UserLoginDto(string Username, string Password);

/// <summary>
/// Represents an authenticated user with their username and access token.
/// </summary>
/// <param name="UserName">The username of the authenticated user.</param>
/// <param name="Token">The access token for the authenticated session.</param>
public record AuthenticatedUserDto(string UserName, string Token);

/// <summary>
/// Represents the data required to request a password reset.
/// </summary>
/// <param name="Email">The email address of the user requesting a password reset.</param>
public record ForgotPasswordDto(string Email);

/// <summary>
/// Represents the data required to reset a user's password.
/// </summary>
/// <param name="Email">The user's email address.</param>
/// <param name="Token">The token required to authorize the password reset.</param>
/// <param name="NewPassword">The new password for the user.</param>
public record ResetPasswordDto(string Email, string Token, string NewPassword);

/// <summary>
/// Represents the data required to enable two-factor authentication for a user.
/// </summary>
/// <param name="UserId">The unique identifier of the user.</param>
public record Enable2FADto(string UserId);

/// <summary>
/// Represents the data required to verify two-factor authentication for a user.
/// </summary>
/// <param name="UserId">The unique identifier of the user.</param>
/// <param name="Token">The verification token for two-factor authentication.</param>
public record Verify2FADto(string UserId, string Token);
