// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Filters.Auth;
using server.src.Application.Includes.Auth;
using server.src.Application.Interfaces.Auth.UserLogouts;
using server.src.Application.Mappings.Auth;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Common;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Services.Auth.UserLogouts;

public class UserLogoutQueries : IUserLogoutQueries
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;
    
    public UserLogoutQueries(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemUserLogoutDto>>> GetLogoutsByUserIdService(Guid id, UrlQuery pageParams, CancellationToken token = default)
    {
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == id};
        var user = await _commonRepository.GetResultByIdAsync(_context.Users, userFilters, userIncludes, token);

        if (user is null)
            return new ListResponse<List<ListItemUserLogoutDto>>()
                .WithMessage("User not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData([]);

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
        var pagedUserLogins = await _commonRepository.GetPagedResultsAsync(_context.UserLogouts, pageParams, filters, includes, token);
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