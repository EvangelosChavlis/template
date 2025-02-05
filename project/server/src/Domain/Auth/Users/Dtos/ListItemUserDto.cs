namespace server.src.Domain.Auth.Users.Dtos;

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
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string PhoneNumber,
    string MobilePhoneNumber
);
