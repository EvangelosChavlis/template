// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Weather.Warnings.Models;

namespace server.src.Application.Weather.Warnings.Filters;

public static class WarningFilters
{
    public static string WarningNameSorting = typeof(Warning).GetProperty(nameof(Warning.Name))!.Name;

    public static Expression<Func<Warning, bool>> WarningSearchFilter(this string filter)
    {
        return o => o.Name.Contains(filter ?? "") ||
            o.Description.Contains(filter ?? "");
    }
}