// source
using server.src.Domain.Auth.Users.Dtos;

namespace server.src.Application.Auth.Users.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemUserDto"/> representing an error state.
/// </summary>
public static class ErrorItemUserDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemUserDto"/> with default values, 
    /// representing an error state when a valid user cannot be retrieved.
    /// </summary>
    /// <returns>An <see cref="ItemUserDto"/> initialized with default values.</returns>
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

            Neighborhood: string.Empty,
            ZipCode: string.Empty,
            Address: string.Empty,
            District: string.Empty,
            Municipality: string.Empty,
            State: string.Empty,
            Region: string.Empty,
            Country: string.Empty,
            Continent: string.Empty,

            PhoneNumber: string.Empty,
            PhoneNumberConfirmed: false,
            MobilePhoneNumber: string.Empty,
            MobilePhoneNumberConfirmed: false,

            Bio: string.Empty,
            DateOfBirth: string.Empty,
            
            IsActive: false,
            Version: Guid.Empty
        );
}