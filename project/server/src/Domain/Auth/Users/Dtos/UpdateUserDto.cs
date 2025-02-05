namespace server.src.Domain.Auth.Users.Dtos;

/// <summary>
/// Represents the data required to update a user.
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
/// <param name="Version">The version of the role for concurrency control during updates.</param>
public record UpdateUserDto(
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
    DateTime DateOfBirth,
    Guid Version
);