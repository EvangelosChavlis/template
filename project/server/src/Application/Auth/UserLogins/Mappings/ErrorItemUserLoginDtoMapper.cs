// source
using server.src.Domain.Auth.UserLogins.Dtos;

namespace server.src.Application.Auth.UserLogins.Mappings;

/// <summary>
/// Provides a default error mapping for <see cref="ItemUserLoginDto"/> when user login data is invalid.
/// </summary>
public static class ErrorItemUserLoginDtoMapper
{
    /// <summary>
    /// Returns a default <see cref="ItemUserLoginDto"/> instance representing an error state.
    /// This is used when mapping a <see cref="UserLogin"/> model fails or returns invalid data.
    /// </summary>
    /// <returns>An <see cref="ItemUserLoginDto"/> with empty or default values.</returns>
    public static ItemUserLoginDto ErrorItemUserLoginDtoMapping()
        => new (
            Id: Guid.Empty,
            LoginProvider: string.Empty,
            ProviderKey: string.Empty,
            ProviderDisplayName: string.Empty,
            Date: string.Empty,
            User: string.Empty
        );
}