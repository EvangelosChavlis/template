// packages
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// source
using server.src.Application.Common.Interfaces;
using server.src.Domain.Auth.UserClaims.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Persistence.Common.Contexts;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Common.Commands;

public record GenerateJwtTokenCommand(
    Guid UserId, 
    string Username, 
    string Email,
    string SecurityStamp
) : IRequest<Response<string>>;

public class GenerateJwtTokenHandler : IRequestHandler<GenerateJwtTokenCommand, Response<string>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtSettings _jwtSettings;
    private readonly RSA _tokenPrivateRsa;
    
    public GenerateJwtTokenHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork,
        JwtSettings jwtSettings)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
        _jwtSettings = jwtSettings;

        _tokenPrivateRsa = RSA.Create();
        _tokenPrivateRsa.ImportFromPem(_jwtSettings.TokenPrivateKey.ToCharArray());
    }

    public async Task<Response<string>> Handle(GenerateJwtTokenCommand command, CancellationToken token = default)
    {
        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);
        
        var roles = await _context.AuthDbSets.Roles
            .Include(r => r.UserRoles)
            .Where(r => r.UserRoles.Any(ur => ur.UserId == command.UserId))
            .ToListAsync(token);

        // Define claims
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, command.UserId.ToString()),
            new(ClaimTypes.Name, command.Username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("SecurityStamp", command.SecurityStamp)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name!)));

        // Signing using RSA private key
        var signingKey = new RsaSecurityKey(_tokenPrivateRsa);
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.RsaSha256);

        var authToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes).ToUniversalTime(),
            signingCredentials: creds
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(authToken);

        var userClaims = new List<UserClaim>();
        foreach (var claim in claims)
        {
            var userClaim = new UserClaim
            {
                UserId = command.UserId,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            };

            userClaims.Add(userClaim);
        }
        var result = await _commonRepository.AddRangeAsync(userClaims, token);

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

        // Initializing object
        return new Response<string>()
            .WithMessage("Success in creating token.")
            .WithStatusCode((int)HttpStatusCode.Created)
            .WithSuccess(result)
            .WithData(jwtToken);
    }

}