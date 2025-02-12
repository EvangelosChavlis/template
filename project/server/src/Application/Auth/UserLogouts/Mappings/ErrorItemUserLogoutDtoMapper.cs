// source
using server.src.Domain.Auth.UserLogouts.Dtos;

namespace server.src.Application.Auth.UserLogouts.Mappings;

/// <summary>
/// Provides a default error mapping for <see cref="ItemUserLogoutDto"/> when user logout data is invalid.
/// </summary>
public static class ErrorItemUserLogoutDtoMapper
{
    /// <summary>
    /// Returns a default <see cref="ItemUserLogoutDto"/> instance representing an error state.
    /// This is used when mapping a <see cref="UserLogout"/> model fails or returns invalid data.
    /// </summary>
    /// <returns>An <see cref="ItemUserLogoutDto"/> with empty or default values.</returns>
    public static ItemUserLogoutDto ErrorItemUserLogoutDtoMapping()
        => new (
            Id: Guid.Empty,
            LoginProvider: string.Empty,
            ProviderKey: string.Empty,
            ProviderDisplayName: string.Empty,
            Date: string.Empty,
            User: string.Empty
        );
}