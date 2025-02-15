// packages
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

// source
using server.src.Application.Common.Interfaces;
using server.src.Domain.Common.Models;

namespace server.src.Application.Common.Queries;

public record GetPrincipalFromExpiredTokenQuery(string Token) : IRequest<ClaimsPrincipal>;

public class GetPrincipalFromExpiredTokenHandler : IRequestHandler<GetPrincipalFromExpiredTokenQuery, ClaimsPrincipal>
{
    private readonly RSA _tokenPublicRsa;
    private readonly JwtSettings _jwtSettings;
    
    public GetPrincipalFromExpiredTokenHandler(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
        
        _tokenPublicRsa = RSA.Create();
        _tokenPublicRsa.ImportFromPem(_jwtSettings.TokenPublicKey.ToCharArray());
    }


    public Task<ClaimsPrincipal> Handle(GetPrincipalFromExpiredTokenQuery query, CancellationToken token = default)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new RsaSecurityKey(_tokenPublicRsa),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(query.Token, tokenValidationParameters, out var securityToken);

        if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.RsaSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return Task.FromResult(principal);
    }

}