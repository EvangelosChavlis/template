// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.UserLogouts.Filters;
using server.src.Application.Auth.UserLogouts.Includes;
using server.src.Application.Auth.UserLogouts.Mappings;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Common;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.UserLogouts.Queries;

public record GetLogoutsByUserIdQuery(Guid Id, UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemUserLogoutDto>>>;

public class GetLogoutsByUserIdHandler : IRequestHandler<GetLogoutsByUserIdQuery, ListResponse<List<ListItemUserLogoutDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetLogoutsByUserIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemUserLogoutDto>>> Handle(GetLogoutsByUserIdQuery query, CancellationToken token = default)
    {
        // Searching Item
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { u => u.Id == query.Id};
        var user = await _commonRepository.GetResultByIdAsync(userFilters, userIncludes, token);

        // Check for existence
        if (user is null)
            return new ListResponse<List<ListItemUserLogoutDto>>()
                .WithMessage("User not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData([]);

        var pageParams = query.UrlQuery;

        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = UserLogoutFilters.UserLogoutDateSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<UserLogout, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.UserLogoutSearchFilter();

        var filters = filter.UserLogoutMatchFilters();

        // Including
        var includes = UserLogoutIncludes.GetUserLogoutIncludes();

        // Paging
        var pagedUserLogins = await _commonRepository.GetPagedResultsAsync(pageParams, filters, includes, token);
        // Mapping
        var dto = pagedUserLogins.Rows.Select(ul => ul.ListItemUserLogoutDtoMapping()).ToList();

        // Determine success 
        var success = pagedUserLogins.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemUserLogoutDto>>()
        {
            Data = dto,
            Success = success,
            Message = success ? "User logouts fetched successfully." : 
                "No user logouts found matching the filter criteria.",
            StatusCode = success ? (int)HttpStatusCode.OK : 
                (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedUserLogins.UrlQuery.TotalRecords,
                PageSize = pagedUserLogins.UrlQuery.PageSize,
                PageNumber = pagedUserLogins.UrlQuery.PageNumber ?? 1,
            }
        };
    }

}
    