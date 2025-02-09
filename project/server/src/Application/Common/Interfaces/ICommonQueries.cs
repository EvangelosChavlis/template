// packages
using System.Security.Claims;

// source
using server.src.Domain.Common.Dtos;

namespace server.src.Application.Common.Interfaces;

public interface ICommonQueries
{
    /// <summary>
    /// Retrieves the current user based on the claims in the HTTP context.
    /// </summary>
    /// <param name="token">Optional cancellation token to allow the operation to be canceled.</param>
    /// <returns>Returns a task representing the asynchronous operation, with the current user’s data as a <see cref="CurrentUserDto"/>. 
    /// If no user is found, it will return a default <see cref="CurrentUserDto"/>.</returns>
    Task<CurrentUserDto> GetCurrentUser(CancellationToken token = default);

    /// <summary>
    /// Decrypts sensitive data using a decryption handler.
    /// </summary>
    /// <param name="encryptedData">The encrypted data to decrypt.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>Returns decrypted data of type T.</returns>
    Task<object> DecryptSensitiveData(string encryptedData, CancellationToken token = default);

    /// <summary>
    /// Encrypts sensitive data using an encryption handler.
    /// </summary>
    /// <param name="data">The data to encrypt.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>Returns the encrypted data as a string.</returns>
    Task<string> EncryptSensitiveData(object data, CancellationToken token = default);

    /// <summary>
    /// Generates a random password with the specified length.
    /// </summary>
    /// <param name="length">The length of the generated password.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>Returns the generated password as a string.</returns>
    Task<string> GeneratePassword(int length, CancellationToken token = default);

    /// <summary>
    /// Retrieves the principal (claims) from an expired JWT token.
    /// </summary>
    /// <param name="authToken">The expired JWT token.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>Returns the claims principal (decoded user data) from the expired token.</returns>
    Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string authToken, CancellationToken token = default);

    /// <summary>
    /// Hashes a password using a hash handler.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>Returns the hashed password as a string.</returns>
    Task<string> HashPassword(string password, CancellationToken token = default);

    /// <summary>
    /// Verifies a password against a stored hashed password.
    /// </summary>
    /// <param name="password">The password to verify.</param>
    /// <param name="storedPasswordHash">The stored hashed password to compare with.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>Returns a boolean indicating if the password matches the stored hash.</returns>
    Task<bool> VerifyPassword(string password, string storedPasswordHash, CancellationToken token = default);
}
