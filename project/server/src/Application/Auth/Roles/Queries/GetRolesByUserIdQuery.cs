// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Roles.Filters;
using server.src.Application.Auth.Roles.Includes;
using server.src.Application.Auth.Roles.Mappings;
using server.src.Application.Auth.Roles.Projections;
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Auth.Roles.Models;
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Auth.Roles.Queries;

public record GetRolesByUserIdQuery(Guid Id, UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemRoleDto>>>;

public class GetRolesByUserIdHandler : IRequestHandler<GetRolesByUserIdQuery, ListResponse<List<ListItemRoleDto>>>
{
    private readonly ICommonRepository _commonRepository;
    
    public GetRolesByUserIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemRoleDto>>> Handle(GetRolesByUserIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        
        if (!validationResult.IsValid)
            return new ListResponse<List<ListItemRoleDto>>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData([]);

        // Searching Item
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == query.Id};
        var user = await _commonRepository.GetResultByIdAsync(userFilters, token: token);

        // Check for existence
        if (user is null)
            return new ListResponse<List<ListItemRoleDto>>()
                .WithMessage("User not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData([]);

        var pageParams = query.UrlQuery;

        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = RoleFilters.RoleNameSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<Role, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.RoleSearchFilter();

        var filters = filter.RoleMatchFilters(user.Id);

        // Including
        var includes = RoleIncludes.GetRolesIncludes();
        // Projection
        var projection = RoleProjections.GetRolesProjection();
        // Paging
        var pagedRoles = await _commonRepository.GetPagedResultsAsync(
            pageParams,
            filters,
            includes,
            projection,
            token
        );
        // Mapping
        var dto = pagedRoles.Rows.Select(r => r.ListItemRoleDtoMapping()).ToList();

        // Determine success 
        var success = pagedRoles.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemRoleDto>>()
        {
            Data = dto,
            Success = success,
            Message = success ? "Roles by user id fetched successfully." : 
                "No roles found matching the filter criteria.",
            StatusCode = success ? (int)HttpStatusCode.OK : 
                (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedRoles.UrlQuery.TotalRecords,
                PageSize = pagedRoles.UrlQuery.PageSize,
                PageNumber = pagedRoles.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}
    