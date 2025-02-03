// packages
using System.Security.Cryptography;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Domain.Models.Auth;

namespace server.src.Application.Common.Queries;

public record HashPasswordQuery(string Password) : IRequest<string>;

public class HashPasswordHandler : IRequestHandler<HashPasswordQuery, string>
{
    private readonly JwtSettings _jwtSettings;
    
    public HashPasswordHandler(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public Task<string> Handle(HashPasswordQuery query, CancellationToken token = default)
    {
        var key = Encoding.UTF8.GetBytes(_jwtSettings.PasswordKey);

        var saltBytes = new byte[64];
        var rng = RandomNumberGenerator.Create();
        rng.GetBytes(saltBytes);

        var salt = Convert.ToBase64String(saltBytes);

        var hmacWithSalt = new HMACSHA256(key);
        var passwordBytes = Encoding.UTF8.GetBytes(query.Password);
        var combined = passwordBytes.Concat(saltBytes).ToArray();
        var extendedHashBytes = hmacWithSalt.ComputeHash(combined);

        var passwordHashed = $"{salt}:{Convert.ToBase64String(extendedHashBytes)}";

        return Task.FromResult(passwordHashed);
    }

}