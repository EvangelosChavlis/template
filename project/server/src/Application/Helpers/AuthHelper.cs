// packages
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// source
using server.src.Domain.Models.Auth;
using server.src.Persistence.Contexts;

namespace server.src.Application.Helpers;

public class AuthHelper : IAuthHelper
{
    private readonly DataContext _context;
    private readonly JwtSettings _jwtSettings;
    
    public AuthHelper(DataContext context, JwtSettings jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings;
    }

    public async Task<string> GenerateJwtToken(User user, CancellationToken token = default)
    {
        var roles = await _context.Roles
            .Include(r => r.UserRoles)
            .Where(r => r.UserRoles.Any(ur => ur.UserId == user.Id))
            .ToListAsync(token);

        // Define claims, including role claims
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),   // User ID
            new(ClaimTypes.Name, user.UserName!),                  // User Name
            new(JwtRegisteredClaimNames.Sub, user.Email!),         // Email
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT ID (unique identifier)
            new("SecurityStamp", user.SecurityStamp)
        };

        // Add role claims
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name!)));

        // Create signing credentials using JwtSettings
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Generate the JWT Token
        var authToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30).ToUniversalTime(),
            signingCredentials: creds);

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(authToken);

        // Persist claims in the UserClaims table (optional)
        foreach (var claim in claims)
        {
            var userClaim = new UserClaim
            {
                UserId = user.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            };

            // Add the claim to the database
            await _context.UserClaims.AddAsync(userClaim, token);
        }

        // Save changes to the database
        await _context.SaveChangesAsync(token);

        return jwtToken;
    }


    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    public string HashPassword(string password)
    {
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

        // Generate a cryptographically secure salt (64 bytes)
        var saltBytes = new byte[64];
        var rng = RandomNumberGenerator.Create();
        rng.GetBytes(saltBytes);

        // Output the salt as a Base64 string
        var salt = Convert.ToBase64String(saltBytes);

        // Create an HMACSHA256 instance with the retrieved key
        using (var hmac = new HMACSHA256(key))
        {
            // Compute the hash of the password with the salt
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = hmac.ComputeHash(passwordBytes);

            // Optionally, generate a longer hash by using the salt in combination with the password
            // Creating a new HMACSHA256 instance to use the combined password and salt as input for hashing
            using (var hmacWithSalt = new HMACSHA256(key))
            {
                var extendedHashBytes = hmacWithSalt.ComputeHash(passwordBytes.Concat(saltBytes).ToArray());

                // Combine the salt and the extended hash into a single string for storage
                // Format: "{salt}:{hash}"
                var passwordHashed = $"{salt}:{Convert.ToBase64String(extendedHashBytes)}";

                return passwordHashed;
            }
        }
    }

    public bool VerifyPassword(string password, string storedPasswordHash)
    {
        var parts = storedPasswordHash.Split(':');
        if (parts.Length != 2)
            return false;

        var salt = Convert.FromBase64String(parts[0]);
        var storedHash = Convert.FromBase64String(parts[1]);

        var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

        // Recompute the HMACSHA256 hash with the provided password and the stored salt
        using (var hmacWithSalt = new HMACSHA256(key))
        {
            // Compute the hash of the password combined with the salt
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var computedHash = hmacWithSalt.ComputeHash(passwordBytes.Concat(salt).ToArray());

            // Compare the computed hash with the stored hash
            return computedHash.SequenceEqual(storedHash); // Returns true if the hashes match
        }
    }


    public string GeneratePassword(int length)
    {
        var random = new Random();
        const string lower = "abcdefghijklmnopqrstuvwxyz";
        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string digits = "0123456789";
        const string nonAlphanumeric = "!@#$%^&*()_-+=<>?";

        var allChars = lower + upper + digits + nonAlphanumeric;
        var password = new char[length];

        password[0] = lower[random.Next(lower.Length)];
        password[1] = upper[random.Next(upper.Length)];
        password[2] = digits[random.Next(digits.Length)];
        password[3] = nonAlphanumeric[random.Next(nonAlphanumeric.Length)];

        for (int i = 4; i < length; i++)
        {
            password[i] = allChars[random.Next(allChars.Length)];
        }

        return new string(password.OrderBy(_ => random.Next()).ToArray());
    }
}