// source
using server.src.Domain.Common.Dtos;

namespace server.src.Application.Common.Interfaces;

/// <summary>
/// Defines common command operations for the application.
/// </summary>
public interface ICommonCommands
{
    /// <summary>
    /// Generates a JSON Web Token (JWT) for a given user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="username">The username of the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <param name="securityStamp">A unique security stamp used for token validation.</param>
    /// <param name="token">An optional cancellation token.</param>
    /// <returns>Returns a <see cref="Response{T}"/> containing the generated JWT as a string.</returns>
    Task<Response<string>> GenerateJwtToken(Guid userId, string username, 
        string email, string securityStamp, CancellationToken token = default);
}
