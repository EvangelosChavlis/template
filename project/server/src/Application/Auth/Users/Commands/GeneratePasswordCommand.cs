// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Helpers;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Users.Commands;

public record GeneratePasswordCommand(Guid Id) : IRequest<Response<string>>;

public class GeneratePasswordHandler : IRequestHandler<GeneratePasswordCommand, Response<string>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;
    private readonly IAuthHelper _authHelper;
    
    public GeneratePasswordHandler(DataContext context, ICommonRepository commonRepository, 
        IAuthHelper authHelper)
    {
        _context = context;
        _commonRepository = commonRepository;
        _authHelper = authHelper;
    }

    public async Task<Response<string>> Handle(GeneratePasswordCommand command, CancellationToken token = default)
    {
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == command.Id };
        var user = await _commonRepository.GetResultByIdAsync(_context.Users, userFilters, userIncludes, token);

        if (user is null)
            return new Response<string>()
                .WithMessage("User not found.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(string.Empty);

        // Generate new password
        var newPassword = _authHelper.GeneratePassword(12);
        user.PasswordHash = _authHelper.HashPassword(newPassword);

        var result = await _context.SaveChangesAsync(token) > 0;
        
        if (!result)
            return new Response<string>()
                .WithMessage("Error in generating password.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to generate password.");

        return new Response<string>()
            .WithMessage("Success in generating password.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData("Password generated successfully.");
    }
}