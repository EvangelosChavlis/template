// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Auth.Roles.Interfaces;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Application.Auth.Roles.Commands;

public record InitializeRolesCommand(List<CreateRoleDto> Dto) : IRequest<Response<string>>;

public class InitializeRolesHandler : IRequestHandler<InitializeRolesCommand, Response<string>>
{
    private readonly IRoleCommands _roleCommands;
    
    public InitializeRolesHandler(IRoleCommands roleCommands)
    {
        _roleCommands = roleCommands;
    }

    public async Task<Response<string>> Handle(InitializeRolesCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();

        foreach (var item in command.Dto)
        {
            var result = await _roleCommands.CreateRoleAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        if (!success)
            return new Response<string>()
                .WithMessage("Error initializing roles.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize roles.");

        return new Response<string>()
            .WithMessage("Success initializing roles.")
            .WithStatusCode((int)HttpStatusCode.Created)
            .WithSuccess(success)
            .WithData(message);
    }
}
    