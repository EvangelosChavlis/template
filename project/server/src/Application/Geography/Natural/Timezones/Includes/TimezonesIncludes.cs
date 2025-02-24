// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Timezones.Models;

namespace server.src.Application.Geography.Natural.Includes.Timezones;

public class TimezonesIncludes
{
    public static IncludeThenInclude<Timezone>[] GetTimezonesIncludes()
    {
        return
        [
            new (t => t.Locations)
        ];
    }
}