// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Roles.Filters;
using server.src.Application.Auth.Roles.Includes;
using server.src.Application.Auth.Roles.Mappings;
using server.src.Application.Auth.Roles.Validators;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Common;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Roles.Queries;

public record GetRolesByUserIdQuery(Guid Id, UrlQuery UrlQuery) : IRequest<ListResponse<List<ItemRoleDto>>>;

public class GetRolesByUserIdHandler : IRequestHandler<GetRolesByUserIdQuery, ListResponse<List<ItemRoleDto>>>
{
    private readonly ICommonRepository _commonRepository;
    
    public GetRolesByUserIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ItemRoleDto>>> Handle(GetRolesByUserIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = RoleValidators.Validate(query.Id);
        
        if (!validationResult.IsValid)
            return new ListResponse<List<ItemRoleDto>>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData([]);

        // Searching Item
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == query.Id};
        var user = await _commonRepository.GetResultByIdAsync(userFilters, userIncludes, token);

        // Check for existence
        if (user is null)
            return new ListResponse<List<ItemRoleDto>>()
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
        var includes = RoleIncludes.GetRolesByUserIdIncludes();

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
    