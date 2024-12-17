// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Models.Errors;

namespace server.src.Application.Filters.Metrics;

public static class ErrorsFiltrers
{
    public static string ErrorNameSorting = typeof(LogError).GetProperty(nameof(LogError.Error))!.Name;

    public static Expression<Func<LogError, bool>> ErrorSearchFilter(this string filter)
    {
        return o => o.Error.Contains(filter ?? "") ||
            o.StatusCode.ToString().Contains(filter ?? "");
    }
}
