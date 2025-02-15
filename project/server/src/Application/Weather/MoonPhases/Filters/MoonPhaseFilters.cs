// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Weather.MoonPhases.Models;

namespace server.src.Application.Weather.MoonPhases.Filters;

public static class MoonPhaseFilters
{
    public static string MoonPhaseNameSorting = typeof(MoonPhase).GetProperty(nameof(MoonPhase.Name))!.Name;

    public static Expression<Func<MoonPhase, bool>> MoonPhaseSearchFilter(this string filter)
    {
        return o => o.Name.Contains(filter ?? "") ||
            o.Description.Contains(filter ?? "");
    }
}