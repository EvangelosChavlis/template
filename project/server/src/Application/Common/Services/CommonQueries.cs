// packages
using System.Security.Claims;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Queries;
using server.src.Domain.Common.Dtos;

namespace server.src.Application.Common.Services;

public class CommonQueries : ICommonQueries
{
    private readonly RequestExecutor _requestExecutor;

    public CommonQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<CurrentUserDto> GetCurrentUser(CancellationToken token = default)
    {
        var query = new CurrentUserQuery();
        return await _requestExecutor.Execute<CurrentUserQuery, CurrentUserDto>(query, token);
    }

    public async Task<object> DecryptSensitiveData(string encryptedData, CancellationToken token = default)
    {
        var query = new DecryptSensitiveDataQuery(encryptedData);
        return await _requestExecutor.Execute<DecryptSensitiveDataQuery, object>(query, token);
    }

    public async Task<string> EncryptSensitiveData(object data, 
        CancellationToken token = default)
    {
        var query = new EncryptSensitiveDataQuery(data);
        return await _requestExecutor.Execute<EncryptSensitiveDataQuery, string>(query, token);
    }

    public async Task<string> GeneratePassword(int length, 
        CancellationToken token = default)
    {
        var query = new GeneratePasswordQuery(length);
        return await _requestExecutor.Execute<GeneratePasswordQuery, string>(query, token);
    }

    public async Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string authToken, 
        CancellationToken token = default)
    {
        var query = new GetPrincipalFromExpiredTokenQuery(authToken);
        return await _requestExecutor.Execute<GetPrincipalFromExpiredTokenQuery, ClaimsPrincipal>(query, token);
    }

    public async Task<string> HashPassword(string password, 
        CancellationToken token = default)
    {
        var query = new HashPasswordQuery(password);
        return await _requestExecutor.Execute<HashPasswordQuery, string>(query, token);
    }

    public async Task<bool> VerifyPassword(string password, 
        string storedPasswordHash, CancellationToken token = default)
    {
        var query = new VerifyPasswordQuery(password, storedPasswordHash);
        return await _requestExecutor.Execute<VerifyPasswordQuery, bool>(query, token);
    }
}
