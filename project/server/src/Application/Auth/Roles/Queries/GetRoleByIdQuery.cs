// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Roles.Mappings;
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Auth.Roles.Models;
using server.src.Domain.Common.Extensions;
using server.src.Domain.Common.Dtos;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Auth.Roles.Queries;

public record GetRoleByIdQuery(Guid Id) : IRequest<Response<ItemRoleDto>>;

public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, Response<ItemRoleDto>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    
    public GetRoleByIdHandler(ICommonRepository commonRepository, ICommonQueries commonQueries)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
    }

    public async Task<Response<ItemRoleDto>> Handle(GetRoleByIdQuery query, CancellationToken token = default)
    {
        // Retrieve current user and check existence
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if (!currentUser.UserFound)
            return new Response<ItemRoleDto>()
                .WithMessage("Current user not found.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemRoleDtoMapper.ErrorItemRoleDtoMapping());

        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemRoleDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemRoleDtoMapper.ErrorItemRoleDtoMapping());

        // Searching Item
        var includes = new Expression<Func<Role, object>>[] { };
        var filters = new Expression<Func<Role, bool>>[] { x => x.Id == query.Id};
        var projection = (Expression<Func<Role, Role>>)(r => new Role 
        { 
            Id = r.Id, 
            Name = r.Name,
            Description = r.Description,
            IsActive = r.IsActive,
            Version = r.Version 
        });
        var role = await _commonRepository.GetResultByIdAsync(filters, includes, 
            projection, token);

        // Check for existence
        if (role is null)
            return new Response<ItemRoleDto>()
                .WithMessage("Role not found.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemRoleDtoMapper.ErrorItemRoleDtoMapping());

        if (role.IsNotLocked())
        {
            var lockResult = await _commonRepository.LockAsync<Role>(role.Id, currentUser.Id, 
                TimeSpan.FromMinutes(30), token);
            if (!lockResult)
                return new Response<ItemRoleDto>()
                    .WithMessage("Failed to lock role.")
                    .WithStatusCode((int)HttpStatusCode.Conflict)
                    .WithSuccess(false)
                    .WithData(ErrorItemRoleDtoMapper.ErrorItemRoleDtoMapping());
        }

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