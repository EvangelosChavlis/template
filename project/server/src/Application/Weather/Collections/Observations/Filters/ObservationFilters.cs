// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Common.Extensions;
using server.src.Domain.Weather.Collections.Observations.Models;

namespace server.src.Application.Weather.Collections.Observations.Filters;

public static class ObservationFilters
{
    public static string ObservationTempSorting = typeof(Observation).GetProperty(nameof(Observation.TemperatureC))!.Name;

    public static Expression<Func<Observation, bool>> ObservationSearchFilter(this string filter)
    {
        return o => o.Timestamp.GetLocalDateTimeString().Contains(filter ?? "") ||
            o.TemperatureC.ToString().Contains(filter ?? "");
    }
}