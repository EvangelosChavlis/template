namespace server.src.Domain.Auth.Users.Dtos;

/// <summary>
/// Represents detailed information about a user, including personal, contact, and geographical details.
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
/// <param name="Neighborhood">The name of the user's neighborhood.</param>
/// <param name="ZipCode">The user's ZIP code.</param>
/// <param name="Address">The user's physical address.</param>
/// <param name="District">The district in which the user resides.</param>
/// <param name="Municipality">The municipality where the user is located.</param>
/// <param name="State">The state of the user's address.</param>
/// <param name="Region">The region of the user's location.</param>
/// <param name="Country">The country of the user's address.</param>
/// <param name="Continent">The continent on which the user resides.</param>
/// <param name="PhoneNumber">The user's phone number.</param>
/// <param name="PhoneNumberConfirmed">Indicates whether the phone number is confirmed.</param>
/// <param name="MobilePhoneNumber">The user's mobile phone number.</param>
/// <param name="MobilePhoneNumberConfirmed">Indicates whether the mobile phone number is confirmed.</param>
/// <param name="Bio">A brief biography of the user.</param>
/// <param name="DateOfBirth">The user's date of birth.</param>
/// <param name="IsActive">Indicates whether the user is active.</param>
/// <param name="Version">The version of the user's information for concurrency control during updates.</param>
public record ItemUserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    bool EmailConfirmed,
    string UserName,
    bool LockoutEnabled,
    string LockoutEnd,
    string InitialPassword,

    string Neighborhood,
    string ZipCode,
    string Address,
    string District,
    string Municipality,
    string State,
    string Region,
    string Country,
    string Continent,

    string PhoneNumber,
    bool PhoneNumberConfirmed,
    string MobilePhoneNumber,
    bool MobilePhoneNumberConfirmed,
    string Bio,
    string DateOfBirth,

    bool IsActive,
    Guid Version
);