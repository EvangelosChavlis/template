namespace server.src.Domain.Auth.Users.Dtos;

/// <summary>
/// Represents the data required to update a user.
/// </summary>
/// <param name="FirstName">The user's first name.</param>
/// <param name="LastName">The user's last name.</param>
/// <param name="Email">The user's email address.</param>
/// <param name="UserName">The user's username.</param>
/// <param name="Password">The user's password.</param>
/// <param name="Address">The user's specific physical address (e.g., street, number).</param>
/// <param name="PhoneNumber">The user's phone number.</param>
/// <param name="MobilePhoneNumber">The user's mobile phone number.</param>
/// <param name="Bio">A brief biography or description of the user.</param>
/// <param name="DateOfBirth">The user's date of birth.</param>
/// <param name="NeighborhoodId">The unique identifier of the neighborhood the user belongs to.</param>
/// <param name="Version">The version token used for concurrency control during updates.</param>
public record UpdateUserDto(
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password,
    string Address,
    string PhoneNumber,
    string MobilePhoneNumber,
    string Bio,
    DateTime DateOfBirth,
    Guid NeighborhoodId,
    Guid Version
);