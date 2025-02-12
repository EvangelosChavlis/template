// source
using server.src.Domain.Auth.Users.Dtos;
using server.src.Domain.Auth.Users.Models;

namespace server.src.Application.Auth.Users.Mappings;

/// <summary>
/// Provides a method to map user creation DTOs to a <see cref="User"/> model.
/// </summary>
public static class CreateUserModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateUserDto"/> and an <see cref="EncryptedUserDto"/> to a new <see cref="User"/> model,
    /// typically for user creation.
    /// </summary>
    /// <param name="userDto">The <see cref="CreateUserDto"/> containing unencrypted user data.</param>
    /// <param name="registered">
    /// A flag indicating whether the user is registered. If true, the initial password is set from registration;
    /// otherwise, it uses the provided password from <paramref name="userDto"/>.
    /// </param>
    /// <param name="encryptedUserDto">The <see cref="EncryptedUserDto"/> containing encrypted user details.</param>
    /// <returns>A new <see cref="User"/> model populated with data from the provided DTOs.</returns>
    public static User CreateUserModelMapping(this CreateUserDto userDto, bool registered, 
        EncryptedUserDto encryptedUserDto) 
    => new()
    {
        // Authentication Information
        UserName = userDto.UserName,
        NormalizedUserName = userDto.UserName.ToUpperInvariant(),
        Email = encryptedUserDto.EmailEncrypted,
        NormalizedEmail = encryptedUserDto.NormalizedEmailEncrypted,
        PasswordHash = encryptedUserDto.PasswordHash,
        EmailConfirmed = false,
        TwoFactorEnabled = false,
        LockoutEnabled = false,
        AccessFailedCount = 0,
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        SecurityStamp = Guid.NewGuid().ToString(),
        LockoutEnd = null,

        // Personal Information
        FirstName = encryptedUserDto.FirstNameEncrypted,
        LastName = encryptedUserDto.LastNameEncrypted,
        InitialPassword = registered ? "from registration" : userDto.Password,

        // Contact Information
        Address = encryptedUserDto.AddressEncrypted,
        ZipCode = encryptedUserDto.ZipCodeEncrypted,
        City = userDto.City,
        State = userDto.State,
        Country = userDto.Country,
        PhoneNumber = encryptedUserDto.PhoneNumberEncrypted,
        PhoneNumberConfirmed = false,
        MobilePhoneNumber = encryptedUserDto.MobilePhoneNumberEncrypted,
        MobilePhoneNumberConfirmed = false,

        // Profile Information
        Bio = encryptedUserDto.BioEncrypted,
        DateOfBirth = encryptedUserDto.DateOfBirthEncrypted,

        // System Information
        IsActive = true,
        Version = Guid.NewGuid()
    };
}
