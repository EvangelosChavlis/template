// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Metrics.LogErrors.Models;

namespace server.src.Application.Metrics.LogErrors.Filters;

public static class LogErrorFiltrers
{
    public static string LogErrorNameSorting = typeof(LogError).GetProperty(nameof(LogError.Error))!.Name;

    public static Expression<Func<LogError, bool>> LogErrorSearchFilter(this string filter)
    {
        return o => o.Error.Contains(filter ?? "") ||
            o.StatusCode.ToString().Contains(filter ?? "");
    }
}
