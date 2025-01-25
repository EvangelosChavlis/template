// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Roles.Filters;
using server.src.Application.Auth.Roles.Includes;
using server.src.Application.Auth.Roles.Mappings;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Common;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Roles.Queries;

public record GetRolesQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ItemRoleDto>>>;

public class GetRolesHandler : IRequestHandler<GetRolesQuery, ListResponse<List<ItemRoleDto>>>
{
    private readonly ICommonRepository _commonRepository;
    
    public GetRolesHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ItemRoleDto>>> Handle(GetRolesQuery query, CancellationToken token = default)
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
        var dto = pagedRoles.Rows.Select(r => r.ItemRoleDtoMapping()).ToList();

        // Determine success 
        var success = pagedRoles.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ItemRoleDto>>()
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