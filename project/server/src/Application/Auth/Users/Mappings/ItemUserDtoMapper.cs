// source
using server.src.Domain.Common.Extensions;
using server.src.Domain.Auth.Users.Dtos;
using server.src.Domain.Auth.Users.Models;

namespace server.src.Application.Auth.Users.Mappings;

/// <summary>
/// Provides extension methods for mapping <see cref="User"/> models to DTOs.
/// </summary>
public static class ItemUserDtoMapper
{
    /// <summary>
    /// Maps a <see cref="User"/> model to an <see cref="ItemUserDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="User"/> model that will be mapped.</param>
    /// <returns>An <see cref="ItemUserDto"/> representing the <see cref="User"/> model.</returns>
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
}
