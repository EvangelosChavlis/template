// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Weather.Collections.MoonPhases.Models;

namespace server.src.Application.Weather.Collections.MoonPhases.Filters;

public static class MoonPhaseFilters
{
    public static string MoonPhaseNameSorting = typeof(MoonPhase).GetProperty(nameof(MoonPhase.Name))!.Name;

    public static Expression<Func<MoonPhase, bool>> MoonPhaseSearchFilter(this string filter)
    {
        return m => m.Name.Contains(filter ?? "") ||
            m.Description.Contains(filter ?? "") ||
            m.Code.Contains(filter ?? "");
    }
}