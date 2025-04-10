namespace server.src.Domain.Auth.UserLogouts.Dtos;

/// <summary>
/// Represents a user's logout details with an external authentication provider.
/// </summary>
/// <param name="Id">The unique identifier of the logout record.</param>
/// <param name="LoginProvider">The authentication provider used for login (e.g., Google, Facebook).</param>
/// <param name="ProviderKey">The unique key provided by the authentication provider to identify the user.</param>
/// <param name="ProviderDisplayName">The display name of the authentication provider.</param>
/// <param name="Date">The date when the login was registered.</param>
/// <param name="User">The identifier of the associated user.</param>
public record ItemUserLogoutDto(
    Guid Id,
    string LoginProvider,
    string ProviderKey,
    string ProviderDisplayName,
    string Date,
    string User
);