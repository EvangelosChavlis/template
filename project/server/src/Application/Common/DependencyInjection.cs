// packages
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Common.Commands;
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Queries;
using server.src.Application.Common.Services;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {        
        // register query handlers
        services.AddScoped<IRequestHandler<DecryptSensitiveDataQuery, object>, DecryptSensitiveDataHandler>();
        services.AddScoped<IRequestHandler<EncryptSensitiveDataQuery, string>, EncryptSensitiveDataHandler>();
        services.AddScoped<IRequestHandler<GeneratePasswordQuery, string>, GeneratePasswordHandler>();
        services.AddScoped<IRequestHandler<GetPrincipalFromExpiredTokenQuery, ClaimsPrincipal>, GetPrincipalFromExpiredTokenHandler>();
        services.AddScoped<IRequestHandler<HashPasswordQuery, string>, HashPasswordHandler>();
        services.AddScoped<IRequestHandler<VerifyPasswordQuery, bool>, VerifyPasswordHandler>();

        // register queries
        services.AddScoped<ICommonQueries, CommonQueries>();

        // register command handlers
        services.AddScoped<IRequestHandler<GenerateJwtTokenCommand, Response<string>>, GenerateJwtTokenHandler>();

        // register commands
        services.AddScoped<ICommonCommands, CommonCommands>();

        return services;
    }
}
