// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Roles.Filters;
using server.src.Application.Auth.Users.Includes;
using server.src.Application.Interfaces;
using server.src.Application.Auth.Users.Mappings;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Common;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Users.Queries;

public record GetUsersQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemUserDto>>>;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, ListResponse<List<ListItemUserDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetUsersHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemUserDto>>> Handle(GetUsersQuery query, CancellationToken token = default)
    {
        var pageParams =  query.UrlQuery;
        
        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = UserFilters.UserNameSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<User, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.UsersSearchFilter();

        var filters = filter.UserMatchFilters();

        // Including
        var includes = UserIncludes.GetUsersIncludes();

        // Paging
        var pagedUsers = await _commonRepository.GetPagedResultsAsync(pageParams, filters, includes, token);
        // Mapping
        var dto = pagedUsers.Rows.Select(a => a.ListItemUserDtoMapping()).ToList();

        // Determine success 
        var success = pagedUsers.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemUserDto>>()
        {
            Data = dto,
            Success = success,
            Message = success ? "Users fetched successfully." : 
                "No users found matching the filter criteria.",
            StatusCode = success ? (int)HttpStatusCode.OK : 
                (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedUsers.UrlQuery.TotalRecords,
                PageSize = pagedUsers.UrlQuery.PageSize,
                PageNumber = pagedUsers.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}