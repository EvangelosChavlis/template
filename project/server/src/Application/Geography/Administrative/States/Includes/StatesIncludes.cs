// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.States.Models;

namespace server.src.Application.Geography.Administrative.Includes.States;

public class StatesIncludes
{
    public static IncludeThenInclude<State>[] GetStatesIncludes()
    {
        return
        [
            new (s => s.Regions)
        ];
    }
}