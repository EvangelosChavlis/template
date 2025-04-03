// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;

namespace server.src.Application.Geography.Natural.Includes.NaturalFeatures;

public class NaturalFeaturesIncludes
{
    public static IncludeThenInclude<NaturalFeature>[] GetNaturalFeaturesIncludes()
    {
        return
        [
            new (nf => nf.Locations)
        ];
    }
}