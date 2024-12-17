// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Extensions;
using server.src.Domain.Models.Weather;

namespace server.src.Application.Filters.Weather;

public static class ForecastFilters
{
    public static string ForecastTempSorting = typeof(Forecast).GetProperty(nameof(Forecast.TemperatureC))!.Name;

    public static Expression<Func<Forecast, bool>> ForecastSearchFilter(this string filter)
    {
        return o => o.Date.GetLocalDateTimeString().Contains(filter ?? "") ||
            o.TemperatureC.ToString().Contains(filter ?? "") ||
            o.Summary.ToString().Contains(filter ?? "");
    }
}