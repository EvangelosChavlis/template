// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Roles.Mappings;
using server.src.Application.Auth.Roles.Validators;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Roles.Queries;

public record GetRoleByIdQuery(Guid Id) : IRequest<Response<ItemRoleDto>>;

public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, Response<ItemRoleDto>>
{
    private readonly ICommonRepository _commonRepository;
    
    public GetRoleByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemRoleDto>> Handle(GetRoleByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = RoleValidators.Validate(query.Id);
        if (!validationResult.IsValid)
            return new Response<ItemRoleDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(RoleMappings.ErrorItemRoleDtoMapping());

        // Searching Item
        var includes = new Expression<Func<Role, object>>[] { };
        var filters = new Expression<Func<Role, bool>>[] { x => x.Id == query.Id};
        var role = await _commonRepository.GetResultByIdAsync(filters, includes, token);

        // Check for existence
        if (role is null)
            return new Response<ItemRoleDto>()
                .WithMessage("Role not found.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(RoleMappings.ErrorItemRoleDtoMapping());

        // Mapping
        var dto = role.ItemRoleDtoMapping();

        // Initializing object
        return new Response<ItemRoleDto>()
            .WithMessage("Role fetched successfully.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}