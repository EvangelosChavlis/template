// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Roles.Filters;
using server.src.Application.Auth.Roles.Includes;
using server.src.Application.Auth.Roles.Mappings;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Auth.Roles.Models;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Roles.Queries;

public record GetRolesQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemRoleDto>>>;

public class GetRolesHandler : IRequestHandler<GetRolesQuery, ListResponse<List<ListItemRoleDto>>>
{
    private readonly ICommonRepository _commonRepository;
    
    public GetRolesHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemRoleDto>>> Handle(GetRolesQuery query, CancellationToken token = default)
    {
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

        var filters = new Expression<Func<Role, bool>>[] { filter! };

        // Including
        var includes = RoleIncludes.GetRolesIncludes();

        // Paging
        var pagedRoles = await _commonRepository.GetPagedResultsAsync(pageParams, filters, includes, token);
        // Mapping
        var dto = pagedRoles.Rows.Select(r => r.ListItemRoleDtoMapping()).ToList();

        // Determine success 
        var success = pagedRoles.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemRoleDto>>()
        {
            Data = dto,
            Success = success,
            Message = success ? "Roles fetched successfully." : 
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