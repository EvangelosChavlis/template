// source
using server.src.Domain.Auth.UserLogins.Dtos;
using server.src.Domain.Auth.UserLogins.Models;
using server.src.Domain.Common.Extensions;

namespace server.src.Application.Auth.UserLogins.Mappings;

/// <summary>
/// Provides mapping extensions for converting <see cref="UserLogin"/> models to <see cref="ItemUserLoginDto"/> DTOs.
/// </summary>
public static class ItemUserLoginDtoMapper
{
    /// <summary>
    /// Maps a <see cref="UserLogin"/> model to a <see cref="ItemUserLoginDto"/> DTO.
    /// </summary>
    /// <param name="model">The <see cref="UserLogin"/> model to map from.</param>
    /// <returns>A <see cref="ItemUserLoginDto"/> containing mapped user login details.</returns>
    public static ItemUserLoginDto ItemUserLoginDtoMapping(this UserLogin model)
        => new (
            Id: model.Id,
            LoginProvider: model.LoginProvider,
            ProviderKey: model.ProviderKey,
            ProviderDisplayName: model.ProviderDisplayName,
            Date: model.Date.GetFullLocalDateTimeString(),
            User: model.User.UserName
        );
}