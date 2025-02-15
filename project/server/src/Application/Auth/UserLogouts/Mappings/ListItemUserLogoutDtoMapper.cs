// source
using server.src.Domain.Auth.UserLogouts.Dtos;
using server.src.Domain.Auth.UserLogouts.Models;
using server.src.Domain.Common.Extensions;

namespace server.src.Application.Auth.UserLogouts.Mappings;

/// <summary>
/// Provides mapping extensions for converting <see cref="UserLogout"/> models to <see cref="ListItemUserLogoutDto"/> DTOs.
/// </summary>
public static class ListItemUserLogoutDtoMapper
{
    /// <summary>
    /// Maps a <see cref="UserLogout"/> model to a <see cref="ListItemUserLogoutDto"/> DTO.
    /// </summary>
    /// <param name="model">The <see cref="UserLogout"/> model to map from.</param>
    /// <returns>A <see cref="ListItemUserLogoutDto"/> containing mapped user logout details.</returns>
    public static ListItemUserLogoutDto ListItemUserLogoutDtoMapping(this UserLogout model)
        => new (
            Id: model.Id,
            LoginProvider: model.LoginProvider!,
            ProviderDisplayName: model.ProviderDisplayName,
            Date: model.Date.GetFullLocalDateTimeString()
        );
}
