// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Weather.Collections;
using server.src.Persistence.Weather.Tools;

namespace server.src.Persistence.Weather;

public static class SetupBuilder
{
    public static void SetupWeather(this ModelBuilder modelBuilder)
    {
       modelBuilder.SetupCollections();
       modelBuilder.SetupTools();
    }
}