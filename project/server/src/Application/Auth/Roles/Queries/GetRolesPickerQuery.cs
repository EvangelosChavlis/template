// packages
using System.Net;

// source
using server.src.Application.Auth.Roles.Mappings;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Roles.Queries;

public record GetRolesPickerQuery() : IRequest<Response<List<PickerRoleDto>>>;

public class GetRolesPickerHandler : IRequestHandler<GetRolesPickerQuery, Response<List<PickerRoleDto>>>
{
    private readonly ICommonRepository _commonRepository;
    
    public GetRolesPickerHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<List<PickerRoleDto>>> Handle(GetRolesPickerQuery query, CancellationToken token = default)
    {
        var roles = await _commonRepository.GetResultPickerAsync<Role>(token);

        // Mapping
        var dto = roles.Select(o => o.PickerRoleDtoMapping()).ToList();

        var success = dto.Count > 0;

        // Initializing object
        return new Response<List<PickerRoleDto>>()
            .WithMessage(success ? "Roles fetched successfully." : 
                "No roles found matching the filter criteria.")
            .WithSuccess(success)
            .WithStatusCode(success ? (int)HttpStatusCode.OK : 
                (int)HttpStatusCode.NotFound)
            .WithData(dto);
    }
}