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
        return s => s.Name.Contains(filter ?? "") ||
            s.Description.Contains(filter ?? "") ||
            s.Code.Contains(filter ?? "");
    }
}