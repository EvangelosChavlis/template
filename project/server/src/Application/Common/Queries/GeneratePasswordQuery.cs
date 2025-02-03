// packages
using System.Security.Cryptography;

// source
using server.src.Application.Common.Interfaces;

namespace server.src.Application.Common.Queries;

public record GeneratePasswordQuery(int Length) : IRequest<string>;

public class GeneratePasswordHandler : IRequestHandler<GeneratePasswordQuery, string>
{
    public Task<string> Handle(GeneratePasswordQuery query, CancellationToken token = default)
    {
        if (query.Length < 4)
            throw new ArgumentException("Password length must be at least 4.", nameof(query.Length));

        using var rng = RandomNumberGenerator.Create();
        
        const string lower = "abcdefghijklmnopqrstuvwxyz";
        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string digits = "0123456789";
        const string nonAlphanumeric = "!@#$%^&*()_-+=<>?";

        var allChars = lower + upper + digits + nonAlphanumeric;
        var password = new char[query.Length];
        var randomBytes = new byte[query.Length];

        rng.GetBytes(randomBytes);

        password[0] = lower[randomBytes[0] % lower.Length];
        password[1] = upper[randomBytes[1] % upper.Length];
        password[2] = digits[randomBytes[2] % digits.Length];
        password[3] = nonAlphanumeric[randomBytes[3] % nonAlphanumeric.Length];

        for (int i = 4; i < query.Length; i++)
            password[i] = allChars[randomBytes[i] % allChars.Length];

        // Shuffle the password to remove predictable patterns
        var result = new string([.. password.OrderBy(_ => randomBytes[_ % query.Length])]);
        
        return Task.FromResult(result);
    }
}