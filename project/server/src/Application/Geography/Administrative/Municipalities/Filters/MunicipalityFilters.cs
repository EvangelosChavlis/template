// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Geography.Administrative.Municipalities.Models;

namespace server.src.Application.Geography.Administrative.Municipalities.Filters;

public static class MunicipalityFilters
{
    public static string MunicipalityNameSorting = 
        typeof(Municipality).GetProperty(nameof(Municipality.Name))!.Name;

    public static Expression<Func<Municipality, bool>> MunicipalitySearchFilter(this string filter)
    {
        return o => o.Name.Contains(filter ?? "") ||
            o.Description.Contains(filter ?? "");
    }
}