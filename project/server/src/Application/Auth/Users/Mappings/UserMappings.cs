// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Auth;
using server.src.Domain.Users.Models;

namespace server.src.Application.Users.Mappings;

/// <summary>
/// Contains static mapping methods to transform between User models and their corresponding DTOs.
/// </summary>
public static class UserMappings
{
    /// <summary>
    /// Maps a User model to a ListItemUserDto.
    /// </summary>
    /// <param name="model">The User model that will be mapped to a ListItemUserDto.</param>
    /// <returns>A ListItemUserDto representing the User model.</returns>
    public static ListItemUserDto ListItemUserDtoMapping(this User model)
        => new (
            Id: model.Id,
            FirstName: model.FirstName,
            LastName: model.LastName,
            Email: model.Email!,
            UserName: model.UserName!,
            PhoneNumber: model.PhoneNumber!,
            MobilePhoneNumber: model.MobilePhoneNumber
        );

    /// <summary>
    /// Maps a User model to an ItemUserDto.
    /// </summary>
    /// <param name="model">The User model that will be mapped to an ItemUserDto.</param>
    /// <returns>An ItemUserDto representing the User model.</returns>
    public static ItemUserDto ItemUserDtoMapping(this User model)
        => new (
            Id: model.Id,
            FirstName: model.FirstName,
            LastName: model.LastName,
            Email: model.Email!,
            EmailConfirmed: model.EmailConfirmed,
            UserName: model.UserName!,
            LockoutEnabled: model.LockoutEnabled,
            LockoutEnd: model.LockoutEnd.ToString()!,
            InitialPassword: model.InitialPassword,
            
            Address: model.Address,
            ZipCode: model.ZipCode,
            City: model.City,
            State: model.State,
            Country: model.Country,
            PhoneNumber: model.PhoneNumber!,
            PhoneNumberConfirmed: model.PhoneNumberConfirmed,
            MobilePhoneNumber: model.MobilePhoneNumber,
            MobilePhoneNumberConfirmed: model.MobilePhoneNumberConfirmed,

            Bio: model.Bio,
            DateOfBirth: model.DateOfBirth.GetLocalDateTimeString(),
            IsActive: model.IsActive,
            Version: Guid.Empty
        );

    /// <summary>
    /// Maps a User model to an ItemUserDto with default values in case of an error.
    /// </summary>
    /// <param name="model">The User model that will be mapped to an ItemUserDto.</param>
    /// <returns>An ItemUserDto with default values representing an error state.</returns>
    public static ItemUserDto ErrorItemUserDtoMapping()
        => new (
            Id: Guid.Empty,
            FirstName: string.Empty,
            LastName: string.Empty,
            Email: string.Empty,
            EmailConfirmed: false,
            UserName: string.Empty,
            LockoutEnabled: false,
            LockoutEnd: string.Empty,
            InitialPassword: string.Empty,

            Address: string.Empty,
            ZipCode: string.Empty,
            City: string.Empty,
            State: string.Empty,
            Country: string.Empty,
            PhoneNumber: string.Empty,
            PhoneNumberConfirmed: false,
            MobilePhoneNumber: string.Empty,
            MobilePhoneNumberConfirmed: false,

            Bio: string.Empty,
            DateOfBirth: string.Empty,
            IsActive: false,
            Version: Guid.Empty
        );


    /// <summary>
    /// Maps a <see cref="UserDto"/> and an <see cref="EncryptedUserDto"/> to a new <see cref="User"/> model,
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

    /// <summary>
    /// Updates an existing User model with data from a UserDto.
    /// </summary>
    /// <param name="dto">The UserDto containing updated data.</param>
    /// <param name="model">The User model to be updated.</param>
    public static void UpdateUserModelMapping(this UpdateUserDto dto, User model)
    {
        model.FirstName = dto.FirstName;
        model.LastName = dto.LastName;
        model.Email = dto.Email;
        model.UserName = dto.UserName;
        model.EmailConfirmed = model.EmailConfirmed;

        model.Address = dto.Address;
        model.ZipCode = dto.ZipCode;
        model.City = dto.City;
        model.State = dto.State;
        model.Country = dto.Country;
        model.PhoneNumber = dto.PhoneNumber;
        model.PhoneNumberConfirmed = model.PhoneNumberConfirmed;
        model.MobilePhoneNumber = dto.MobilePhoneNumber;
        model.MobilePhoneNumberConfirmed = model.MobilePhoneNumberConfirmed;
        
        model.Bio = dto.Bio;
        model.DateOfBirth = dto.DateOfBirth;
    }
}