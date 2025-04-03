// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Weather.Collections;

namespace server.src.Persistence.Weather;

public class WeatherDbSets
{
    public CollectionsDbSets CollectionsDbSets { get; private set; }
    public ToolsDbSets ToolsDbSets { get; private set; }

    public WeatherDbSets(DbContext context)
    {
        CollectionsDbSets = new CollectionsDbSets(context);
        ToolsDbSets = new ToolsDbSets(context);
    }
}