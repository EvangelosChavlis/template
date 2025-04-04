// source
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Domain.Geography.Natural.Locations.Extensions;

public static class LocationExtensions
{
    public static string GetCoordinates(this Location location)
        => @$"(lat: {location.Latitude}, 
            long: {location.Longitude}, 
            alt: {location.Altitude}, 
            depth: {location.Depth})"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim();
}
