// source
using server.src.Domain.Auth.UserLogins.Dtos;
using server.src.Domain.Auth.UserLogins.Models;
using server.src.Domain.Common.Extensions;

namespace server.src.Application.Auth.UserLogins.Mappings;

/// <summary>
/// Provides mapping extensions for converting <see cref="UserLogin"/> models to <see cref="ListItemUserLoginDto"/> DTOs.
/// </summary>
public static class ListItemUserLoginDtoMapper
{
    /// <summary>
    /// Maps a <see cref="UserLogin"/> model to a <see cref="ListItemUserLoginDto"/> DTO.
    /// </summary>
    /// <param name="model">The <see cref="UserLogin"/> model to map from.</param>
    /// <returns>A <see cref="ListItemUserLoginDto"/> containing mapped user login details.</returns>
    public static ListItemUserLoginDto ListItemUserLoginDtoMapping(this UserLogin model)
        => new (
            Id: model.Id,
            LoginProvider: model.LoginProvider!,
            ProviderDisplayName: model.ProviderDisplayName,
            Date: model.Date.GetFullLocalDateTimeString()
        );
}
