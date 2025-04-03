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
    
    public static double Haversine(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371e3; // Earth radius in meters
        var phi1 = lat1 * Math.PI / 180;
        var phi2 = lat2 * Math.PI / 180;
        var deltaPhi = (lat2 - lat1) * Math.PI / 180;
        var deltaLambda = (lon2 - lon1) * Math.PI / 180;

        var a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) +
                Math.Cos(phi1) * Math.Cos(phi2) *
                Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return R * c;
    }
}
