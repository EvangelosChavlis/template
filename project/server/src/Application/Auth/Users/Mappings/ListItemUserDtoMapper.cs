// source
using server.src.Domain.Auth.Users.Dtos;
using server.src.Domain.Auth.Users.Models;

namespace server.src.Application.Auth.Users.Mappings;

/// <summary>
/// Provides extension methods for mapping User models to DTOs.
/// </summary>
public static class ListItemUserDtoMapper
{
    /// <summary>
    /// Maps a <see cref="User"/> model to a <see cref="ListItemUserDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="User"/> model to be mapped.</param>
    /// <returns>A <see cref="ListItemUserDto"/> representing the <see cref="User"/> model.</returns>
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
}
