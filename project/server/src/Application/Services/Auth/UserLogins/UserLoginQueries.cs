// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Filters.Auth;
using server.src.Application.Includes.Auth;
using server.src.Application.Interfaces.Auth.UserLogins;
using server.src.Application.Mappings.Auth;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Common;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Services.Auth.UserLogins;

public class UserLoginQueries : IUserLoginQueries
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;
    
    public UserLoginQueries(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemUserLoginDto>>> GetLoginsByUserIdService(Guid id, UrlQuery pageParams, CancellationToken token = default)
    {
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == id};
        var user = await _commonRepository.GetResultByIdAsync(_context.Users, userFilters, userIncludes, token);

        if (user is null)
            return new ListResponse<List<ListItemUserLoginDto>>()
                .WithMessage("User not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData([]);

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
        var pagedUserLogins = await _commonRepository.GetPagedResultsAsync(_context.UserLogins, pageParams, filters, includes, token);
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