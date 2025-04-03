// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Geography.Administrative.Stations.Models;

namespace server.src.Application.Geography.Administrative.Stations.Filters;

public static class StationFilters
{
    public static string StationNameSorting = 
        typeof(Station).GetProperty(nameof(Station.Name))!.Name;

    public static Expression<Func<Station, bool>> StationSearchFilter(this string filter)
    {
        return s => s.Name.Contains(filter ?? "") ||
            s.Description.Contains(filter ?? "") ||
            s.Code.Contains(filter ?? "");
    }
}