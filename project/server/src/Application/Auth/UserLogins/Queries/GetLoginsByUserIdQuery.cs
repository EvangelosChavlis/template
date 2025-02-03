// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.UserLogins.Filters;
using server.src.Application.Auth.UserLogins.Includes;
using server.src.Application.Auth.UserLogins.Mappings;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Common;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.UserLogins.Queries;

public record GetLoginsByUserIdQuery(Guid Id, UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemUserLoginDto>>>;

public class GetLoginsByUserIdHandler : IRequestHandler<GetLoginsByUserIdQuery, ListResponse<List<ListItemUserLoginDto>>>
{
    private readonly ICommonRepository _commonRepository;
    
    public GetLoginsByUserIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemUserLoginDto>>> Handle(GetLoginsByUserIdQuery query, CancellationToken token = default)
    {
        // Searching Item
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { u => u.Id == query.Id};
        var user = await _commonRepository.GetResultByIdAsync(userFilters, userIncludes, token);

        // Check for existence
        if (user is null)
            return new ListResponse<List<ListItemUserLoginDto>>()
                .WithMessage("User not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData([]);

        var pageParams = query.UrlQuery;

        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = UserLoginFilters.UserLoginDateSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<UserLogin, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.UserLoginSearchFilter();

        var filters = filter.UserLoginMatchFilters();

        // Including
        var includes = UserLoginIncludes.GetUserLoginsIncludes();

        // Paging
        var pagedUserLogins = await _commonRepository.GetPagedResultsAsync(pageParams, filters, includes, token);
        // Mapping
        var dto = pagedUserLogins.Rows.Select(ul => ul.ListItemUserLoginDtoMapping()).ToList();

        // Determine success 
        var success = pagedUserLogins.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemUserLoginDto>>()
        {
            Data = dto,
            Success = success,
            Message = success ? "User logins fetched successfully." : 
                "No user logins found matching the filter criteria.",
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