// packages
using System.Security.Claims;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Queries;
using server.src.Domain.Common.Dtos;

namespace server.src.Application.Common.Services;

public class CommonQueries : ICommonQueries
{
    private readonly IRequestHandler<CurrentUserQuery, CurrentUserDto> _currentUserHandler;
    private readonly IRequestHandler<DecryptSensitiveDataQuery, object> _decryptSensitiveDataHandler;
    private readonly IRequestHandler<EncryptSensitiveDataQuery, string> _encryptSensitiveDataHandler;
    private readonly IRequestHandler<GeneratePasswordQuery, string> _generatePasswordHandler;
    private readonly IRequestHandler<GetPrincipalFromExpiredTokenQuery, ClaimsPrincipal> _getPrincipalFromExpiredTokenHandler;
    private readonly IRequestHandler<HashPasswordQuery, string> _hashPasswordHandler;
    private readonly IRequestHandler<VerifyPasswordQuery, bool> _verifyPasswordHandler;

    public CommonQueries(
        IRequestHandler<CurrentUserQuery, CurrentUserDto> currentUserHandler,
        IRequestHandler<DecryptSensitiveDataQuery, object> decryptSensitiveDataHandler,
        IRequestHandler<EncryptSensitiveDataQuery, string> encryptSensitiveDataHandler,
        IRequestHandler<GeneratePasswordQuery, string> generatePasswordHandler,
        IRequestHandler<GetPrincipalFromExpiredTokenQuery, ClaimsPrincipal> getPrincipalFromExpiredTokenHandler,
        IRequestHandler<HashPasswordQuery, string> hashPasswordHandler,
        IRequestHandler<VerifyPasswordQuery, bool> verifyPasswordHandler)
    {
        _currentUserHandler = currentUserHandler;
        _decryptSensitiveDataHandler = decryptSensitiveDataHandler;
        _encryptSensitiveDataHandler = encryptSensitiveDataHandler;
        _generatePasswordHandler = generatePasswordHandler;
        _getPrincipalFromExpiredTokenHandler = getPrincipalFromExpiredTokenHandler;
        _hashPasswordHandler = hashPasswordHandler;
        _verifyPasswordHandler = verifyPasswordHandler;
    }

    public async Task<CurrentUserDto> GetCurrentUser(CancellationToken token = default)
    {
        var query = new CurrentUserQuery();
        return await _currentUserHandler.Handle(query, token);
    }

    public async Task<object> DecryptSensitiveData(string encryptedData, CancellationToken token = default)
    {
        var query = new DecryptSensitiveDataQuery(encryptedData);
        return await _decryptSensitiveDataHandler.Handle(query, token);
    }

    public async Task<string> EncryptSensitiveData(object data, 
        CancellationToken token = default)
    {
        var query = new EncryptSensitiveDataQuery(data);
        return await _encryptSensitiveDataHandler.Handle(query, token);
    }

    public async Task<string> GeneratePassword(int length, 
        CancellationToken token = default)
    {
        var query = new GeneratePasswordQuery(length);
        return await _generatePasswordHandler.Handle(query, token);
    }

    public async Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string authToken, 
        CancellationToken token = default)
    {
        var query = new GetPrincipalFromExpiredTokenQuery(authToken);
        return await _getPrincipalFromExpiredTokenHandler.Handle(query, token);
    }

    public async Task<string> HashPassword(string password, 
        CancellationToken token = default)
    {
        var query = new HashPasswordQuery(password);
        return await _hashPasswordHandler.Handle(query, token);
    }

    public async Task<bool> VerifyPassword(string password, 
        string storedPasswordHash, CancellationToken token = default)
    {
        var query = new VerifyPasswordQuery(password, storedPasswordHash);
        return await _verifyPasswordHandler.Handle(query, token);
    }
}
