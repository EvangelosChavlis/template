// packages
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// source
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Helpers;

public class AuthHelper : IAuthHelper
{
    private readonly DataContext _context;
    private readonly JwtSettings _jwtSettings;
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly RSA _privateRsa;
    private readonly RSA _publicRsa;
    
    public AuthHelper(DataContext context, JwtSettings jwtSettings, 
        ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _context = context;
        _jwtSettings = jwtSettings;
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;

        // Load RSA keys from the settings
        _privateRsa = RSA.Create();
        _privateRsa.ImportFromPem(_jwtSettings.PrivateKey.ToCharArray());

        _publicRsa = RSA.Create();
        _publicRsa.ImportFromPem(_jwtSettings.PublicKey.ToCharArray());
    }

    public async Task<Response<string>> GenerateJwtToken(User user, CancellationToken token = default)
    {
        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);
        
        var roles = await _context.Roles
            .Include(r => r.UserRoles)
            .Where(r => r.UserRoles.Any(ur => ur.UserId == user.Id))
            .ToListAsync(token);

        // Define claims
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),  // User ID
            new(ClaimTypes.Name, user.UserName!),                 // User Name
            new(JwtRegisteredClaimNames.Sub, user.Email!),        // Email
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT ID
            new("SecurityStamp", user.SecurityStamp)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name!)));

        // Signing using RSA private key
        var signingKey = new RsaSecurityKey(_privateRsa);
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.RsaSha256);

        var authToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30).ToUniversalTime(),
            signingCredentials: creds
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(authToken);

        var result = true;
        foreach (var claim in claims)
        {
            var userClaim = new UserClaim
            {
                UserId = user.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            };

            result &= await _commonRepository.AddAsync(userClaim, token);
        }

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error in persisting user claims.")
                .WithSuccess(result)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("The production of token failed");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        return new Response<string>()
            .WithMessage("Success in creating token.")
            .WithStatusCode((int)HttpStatusCode.Created)
            .WithSuccess(result)
            .WithData(jwtToken);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new RsaSecurityKey(_publicRsa),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.RsaSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    public string HashPassword(string password)
    {
        var key = Encoding.UTF8.GetBytes(_jwtSettings.PasswordKey);

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
                var passwordHashed = $"{salt}:{Convert.ToBase64String(extendedHashBytes)}";

                return passwordHashed;
            }
        }
    }

    public string HashSensitiveData(string data)
    {
        var key = Encoding.UTF8.GetBytes(_jwtSettings.PasswordKey);

        // Generate a cryptographically secure salt (64 bytes)
        var saltBytes = new byte[64];
        var rng = RandomNumberGenerator.Create();
        rng.GetBytes(saltBytes);

        // Output the salt as a Base64 string
        var salt = Convert.ToBase64String(saltBytes);

        // Create an HMACSHA256 instance with the retrieved key
        using (var hmac = new HMACSHA256(key))
        {
            // Compute the hash of the data with the salt
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var hashBytes = hmac.ComputeHash(dataBytes);

            // Optionally, generate a longer hash by using the salt in combination with the password
            // Creating a new HMACSHA256 instance to use the combined password and salt as input for hashing
            using (var hmacWithSalt = new HMACSHA256(key))
            {
                var extendedHashBytes = hmacWithSalt.ComputeHash(dataBytes.Concat(saltBytes).ToArray());

                // Combine the salt and the extended hash into a single string for storage
                var dataHashed = $"{salt}:{Convert.ToBase64String(extendedHashBytes)}";

                return dataHashed;
            }
        }
    }

    // Verify the password against the stored hash
    public bool VerifyPassword(string password, string storedPasswordHash)
    {
        var parts = storedPasswordHash.Split(':');
        if (parts.Length != 2)
            return false;

        var salt = Convert.FromBase64String(parts[0]);
        var storedHash = Convert.FromBase64String(parts[1]);

        var key = Encoding.UTF8.GetBytes(_jwtSettings.PasswordKey);

        // Recompute the HMACSHA256 hash with the provided password and the stored salt
        using (var hmacWithSalt = new HMACSHA256(key))
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var computedHash = hmacWithSalt.ComputeHash(passwordBytes.Concat(salt).ToArray());

            return computedHash.SequenceEqual(storedHash); // Returns true if the hashes match
        }
    }

    // Verify the password against the stored hash
    public bool VerifySensitiveData(string data, string storedDataHash)
    {
        var parts = storedDataHash.Split(':');
        if (parts.Length != 2)
            return false;

        var salt = Convert.FromBase64String(parts[0]);
        var storedHash = Convert.FromBase64String(parts[1]);

        var key = Encoding.UTF8.GetBytes(_jwtSettings.PasswordKey);

        // Recompute the HMACSHA256 hash with the provided password and the stored salt
        using (var hmacWithSalt = new HMACSHA256(key))
        {
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var computedHash = hmacWithSalt.ComputeHash(dataBytes.Concat(salt).ToArray());

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