// source
using server.src.Application.Common.Commands;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Common.Services;

public class CommonCommands : ICommonCommands
{
    private readonly IRequestHandler<GenerateJwtTokenCommand, Response<string>> _generateJwtTokenHandler;

    public CommonCommands(IRequestHandler<GenerateJwtTokenCommand, Response<string>> generateJwtTokenHandler)
    {
        _generateJwtTokenHandler = generateJwtTokenHandler;
    }

    public async Task<Response<string>> GenerateJwtToken(Guid userId, string username, 
        string email, string securityStamp, CancellationToken token = default)
    {
        var query = new GenerateJwtTokenCommand(userId, username, email, securityStamp);
        return await _generateJwtTokenHandler.Handle(query, token);
    }
}
