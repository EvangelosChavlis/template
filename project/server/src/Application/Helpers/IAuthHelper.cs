// packages
using System.Security.Claims;
using server.src.Domain.Dto.Common;


// source
using server.src.Domain.Models.Auth;

namespace server.src.Application.Helpers;

/// <summary>
/// Provides authentication-related helper methods, including JWT token generation,
/// password hashing and verification, and password generation.
/// </summary>
public interface IAuthHelper
{
    /// <summary>
    /// Generates a JWT token for the given user.
    /// </summary>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <param name="token">A cancellation token.</param>
    /// <returns>A JWT token as a string.</returns>
    Task<Response<string>> GenerateJwtToken(User user, CancellationToken token = default);

    /// <summary>
    /// Extracts claims from an expired JWT token.
    /// </summary>
    /// <param name="token">The expired JWT token.</param>
    /// <returns>A <see cref="ClaimsPrincipal"/> containing the claims of the token.</returns>
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    /// <summary>
    /// Hashes a plaintext password using a secure hashing algorithm.
    /// </summary>
    /// <param name="password">The plaintext password to hash.</param>
    /// <returns>A hashed password string.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verifies a plaintext password against a stored hashed password.
    /// </summary>
    /// <param name="password">The plaintext password to verify.</param>
    /// <param name="storedPasswordHash">The stored hashed password.</param>
    /// <returns>True if the password matches the hash; otherwise, false.</returns>
    bool VerifyPassword(string password, string storedPasswordHash);

    /// <summary>
    /// Generates a random password of the specified length.
    /// </summary>
    /// <param name="length">The desired length of the password.</param>
    /// <returns>A randomly generated password.</returns>
    string GeneratePassword(int length);
}