// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Geography.Administrative.States.Models;

namespace server.src.Application.Geography.Administrative.States.Filters;

public static class StateFilters
{
    public static string StateNameSorting = 
        typeof(State).GetProperty(nameof(State.Name))!.Name;

    public static Expression<Func<State, bool>> StateSearchFilter(this string filter)
    {
        return o => o.Name.Contains(filter ?? "") ||
            o.Description.Contains(filter ?? "");
    }
}