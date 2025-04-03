// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Weather.Collections.Warnings.Models;

namespace server.src.Application.Weather.Collections.Warnings.Filters;

public static class WarningFilters
{
    public static string WarningNameSorting = typeof(Warning).GetProperty(nameof(Warning.Name))!.Name;

    public static Expression<Func<Warning, bool>> WarningSearchFilter(this string filter)
    {
        return w => w.Name.Contains(filter ?? "") ||
            w.Description.Contains(filter ?? "") ||
            w.Code.Contains(filter ?? "");
    }
}