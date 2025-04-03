// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Filters;

public static class NaturalFeatureFilters
{
    public static string NaturalFeatureNameSorting = typeof(NaturalFeature).GetProperty(nameof(NaturalFeature.Name))!.Name;

    public static Expression<Func<NaturalFeature, bool>> NaturalFeatureSearchFilter(this string filter)
    {
        return nf => nf.Name.Contains(filter ?? "") ||
            nf.Code.Contains(filter ?? "");
    }
}