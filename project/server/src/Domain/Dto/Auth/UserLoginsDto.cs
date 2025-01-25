namespace server.src.Domain.Dto.Auth;

/// <summary>
/// Represents a user's login details with external authentication providers.
/// </summary>
/// <param name="Id">The unique identifier of the login record.</param>
/// <param name="LoginProvider">The authentication provider used for login (e.g., Google, Facebook).</param>
/// <param name="ProviderDisplayName">The display name of the authentication provider.</param>
/// <param name="Date">The date when the login was registered.</param>
public record ListItemUserLoginDto(
    Guid Id,
    string LoginProvider,
    string ProviderDisplayName,
    string Date
);
