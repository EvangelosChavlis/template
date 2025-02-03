// packages
using System.Security.Cryptography;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Domain.Models.Auth;

namespace server.src.Application.Common.Queries;

public record VerifyPasswordQuery(
    string Password, 
    string StoredPasswordHash
) : IRequest<bool>;

public class VerifyPasswordHandler : IRequestHandler<VerifyPasswordQuery, bool>
{
    private readonly JwtSettings _jwtSettings;

    public VerifyPasswordHandler(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public Task<bool> Handle(VerifyPasswordQuery query, CancellationToken token = default)
    {
        var parts = query.StoredPasswordHash.Split(':');
        if (parts.Length != 2)
            return Task.FromResult(false);

        var salt = Convert.FromBase64String(parts[0]);
        var storedHash = Convert.FromBase64String(parts[1]);

        var key = Encoding.UTF8.GetBytes(_jwtSettings.PasswordKey);
        var hmacWithSalt = new HMACSHA256(key);
        var passwordBytes = Encoding.UTF8.GetBytes(query.Password);
    
        var combined = passwordBytes.Concat(salt).ToArray();
        var computedHash = hmacWithSalt.ComputeHash(combined);

        var verified = computedHash.SequenceEqual(storedHash);

        return Task.FromResult(verified);
    }
}