// source
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Domain.Geography.Natural.Locations.Extensions;

public static class LocationExtensions
{
    public static string GetCoordinates(this Location location)
        => $"({location.Latitude}, {location.Longitude}, {location.Altitude})";
}
