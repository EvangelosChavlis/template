// packages
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

// source
using server.src.Application.Common.Interfaces;
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Common.Dtos;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Common.Queries;

public record CurrentUserQuery : IRequest<CurrentUserDto>;

public class CurrentUserHandler : IRequestHandler<CurrentUserQuery, CurrentUserDto>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    

    public CurrentUserHandler(ICommonRepository commonRepository, IHttpContextAccessor httpContextAccessor)
    {
        _commonRepository = commonRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CurrentUserDto> Handle(CurrentUserQuery query, CancellationToken token = default)
    {
        var userHttpCotext = _httpContextAccessor.HttpContext?.User;

        if (userHttpCotext is null)
            return new CurrentUserDto(Guid.Empty, string.Empty, false);

        var userIdClaim = userHttpCotext.FindFirstValue(ClaimTypes.NameIdentifier);
        var userNameClaim = userHttpCotext.FindFirstValue(ClaimTypes.Name);

        if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(userNameClaim) || 
            !Guid.TryParse(userIdClaim, out var userId))
        {
            return new CurrentUserDto(Guid.Empty, string.Empty, false);
        }

        // Searching Item
        var includes = new Expression<Func<User, object>>[] { };
        var filters = new Expression<Func<User, bool>>[] { u => u.Id == userId};
        var projection = (Expression<Func<User, User>>)(u => new User { Id = u.Id, UserName = u.UserName });
        var user = await _commonRepository.GetResultByIdAsync(filters, includes, projection, token);

        // Check for existence
        if (user is null)
            return new CurrentUserDto(Guid.Empty, string.Empty, false);

        return new CurrentUserDto(user.Id, user.UserName, true);
    }
}
