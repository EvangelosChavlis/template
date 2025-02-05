
using server.src.Domain.Users.Models;

namespace server.src.Application.Auth.Users.Mappings;

public static class ListItemUserDtoMapper
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
}
