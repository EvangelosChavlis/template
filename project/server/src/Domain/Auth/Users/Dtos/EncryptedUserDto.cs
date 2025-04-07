namespace server.src.Domain.Auth.Users.Dtos;

/// <summary>
/// Represents an encrypted user data transfer object (DTO) used for securely storing user details.
/// </summary>
/// <param name="FirstNameEncrypted">The encrypted first name of the user.</param>
/// <param name="LastNameEncrypted">The encrypted last name of the user.</param>
/// <param name="EmailEncrypted">The encrypted email address of the user.</param>
/// <param name="NormalizedEmailEncrypted">The encrypted normalized email address of the user.</param>
/// <param name="PasswordHash">The securely hashed password of the user.</param>
/// <param name="AddressEncrypted">The encrypted physical address of the user.</param>
/// <param name="PhoneNumberEncrypted">The encrypted phone number of the user.</param>
/// <param name="MobilePhoneNumberEncrypted">The encrypted mobile phone number of the user.</param>
/// <param name="BioEncrypted">The encrypted biography or additional user information.</param>
/// <param name="DateOfBirthEncrypted">The encrypted date of birth of the user.</param>
public record EncryptedUserDto(
    string FirstNameEncrypted,
    string LastNameEncrypted,
    string EmailEncrypted,
    string NormalizedEmailEncrypted,
    string PasswordHash,
    string AddressEncrypted,
    string PhoneNumberEncrypted,
    string MobilePhoneNumberEncrypted,
    string BioEncrypted,
    string DateOfBirthEncrypted
);