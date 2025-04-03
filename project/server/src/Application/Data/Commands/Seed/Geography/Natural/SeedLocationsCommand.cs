// packages
using System.Linq.Expressions;
using System.Net;
using GeoTimeZone;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.Locations.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Models;
using server.src.Domain.Geography.Natural.Locations.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;
using server.src.Domain.Geography.Natural.Timezones.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Data.Commands.Seed.Geography.Natural;

public record SeedLocationsCommand() : IRequest<Response<string>>;

public class SeedLocationsHandler : IRequestHandler<SeedLocationsCommand, Response<string>>
{
    private readonly ILocationCommands _locationCommands;
    private readonly ICommonRepository _commonRepository;
    private readonly double _resolution = 0.01;

    public SeedLocationsHandler(ILocationCommands locationCommands, 
        ICommonRepository commonRepository)
    {
        _locationCommands = locationCommands;
        _commonRepository = commonRepository;
    }

    public async Task<Response<string>> Handle(SeedLocationsCommand command, CancellationToken token = default)
    {
        var lonStep = _resolution;
        var latStep = _resolution;

        const int batchSize = 100000;

        var i = 0;

        var failures = new List<string>();
        var locationDtos = new List<CreateLocationDto>();

        for (var lon = -180.0; lon <= 180.0; lon += lonStep)
        {
            for (var lat = -90.0; lat <= 90.0; lat += latStep)
            {

                var alt = GetElevation(lat, lon);
                var cz = DetermineClimateZone(lat, lon, alt);
                var tz = DetermineTimezone(lat, lon);
                var st = DetermineSurfaceType(lat, lon, alt);
                var depth = DetermineDepth(lat, lon, st);
                var nf = "N_A";

                var climateZoneProjection = (Expression<Func<ClimateZone, Guid>>)(c => c.Id);
                var climateZoneFilters = new Expression<Func<ClimateZone, bool>>[] { c => c.Code == cz};
                var climateZoneId = await _commonRepository.GetResultByIdAsync(
                    filters: climateZoneFilters,
                    projection: climateZoneProjection,
                    token: token
                );
                if (climateZoneId == Guid.Empty)
                {
                    failures.Add($"Climate zone with code {cz} not found");
                    continue;
                }
                    
                var timezoneProjection = (Expression<Func<Timezone, Guid>>)(t => t.Id);
                var timezoneFilters = new Expression<Func<Timezone, bool>>[]{ t => t.Code == tz };
                var timezoneId = await _commonRepository.GetResultByIdAsync(
                    filters: timezoneFilters, 
                    projection: timezoneProjection, 
                    token: token);
                if (timezoneId == Guid.Empty)
                {
                    failures.Add($"Timezone with code {tz} not found");
                    continue;
                }

                var surfaceTypeProjection = (Expression<Func<SurfaceType, Guid>>)(s => s.Id);
                var surfaceTypeFilters = new Expression<Func<SurfaceType, bool>>[]{ s => s.Code == st };
                var surfaceTypeId = await _commonRepository.GetResultByIdAsync(
                    filters: surfaceTypeFilters,
                    projection: surfaceTypeProjection,
                    token: token
                );
                if (surfaceTypeId == Guid.Empty)
                {
                    failures.Add($"Surface type with code {st} not found");
                    continue;
                }

                var naturalFeatureProjection = (Expression<Func<NaturalFeature, Guid>>)(n => n.Id);
                var naturalFeatureFilters = new Expression<Func<NaturalFeature, bool>>[]{ n => n.Code == nf };
                var naturalFeatureId = await _commonRepository.GetResultByIdAsync(
                    projection: naturalFeatureProjection,
                    filters: naturalFeatureFilters, 
                    token: token
                );
                if (naturalFeatureId == Guid.Empty)
                {
                    failures.Add($"Natural feature with code {nf} not found");
                    continue;
                }

                Console.WriteLine(i+")");
                Console.WriteLine($"Longitude: {Math.Round(lon, 4)}");
                Console.WriteLine($"Latitude: {Math.Round(lat, 4)}");
                Console.WriteLine($"Altitude: {Math.Round(alt, 4)}");
                Console.WriteLine($"Depth: {Math.Round(depth, 4)}");
                Console.WriteLine($"TimezoneId: {timezoneId}");
                Console.WriteLine($"NaturalFeatureId: {naturalFeatureId}");
                Console.WriteLine($"SurfaceTypeId: {surfaceTypeId}");
                Console.WriteLine($"ClimateZoneId: {climateZoneId}");
                Console.WriteLine($"Date: {DateTime.Now}");
                Console.WriteLine("==============================");
                i++;

                var locationDto = new CreateLocationDto(
                    Longitude: lon,
                    Latitude: lat,
                    Altitude: alt,
                    Depth: depth,
                    TimezoneId: timezoneId,
                    NaturalFeatureId: naturalFeatureId,
                    SurfaceTypeId: surfaceTypeId,
                    ClimateZoneId: climateZoneId
                );

                locationDtos.Add(locationDto);
                if (locationDtos.Count >= batchSize)
                {
                    var result = await _locationCommands.InitializeLocationsAsync(locationDtos, token);
                    failures.AddRange(result.Failures);
                    locationDtos.Clear();
                }

            }
        }

        if (locationDtos.Count > 0)
        {
            var result = await _locationCommands.InitializeLocationsAsync(locationDtos, token);
            Console.WriteLine(result.Success);
            failures.AddRange(result.Failures);
        }

        return new Response<string>()
            .WithMessage("Success in locations seeding")
            .WithSuccess(true)
            .WithFailures(failures)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithData("Locations seeding was successful!");
    }

    public string DetermineSurfaceType(double lat, double lon, double elevation)
    {
        // High-altitude areas (Mountains)
        if (elevation > 2500)
            return "MNT"; // Mountainous

        // Polar regions
        if (lat >= 66.5 || lat <= -66.5)
            return "POL"; // Polar

        if (lat >= -23.5 && lat <= 23.5)
        {
            if (elevation < 500) return "RFR"; // More likely to be dense rainforest
            if (elevation < 1500) return "SVN"; // Savanna range
            return "MNT"; // Higher elevations might be mountainous
        }

        // Desert classification (more precise)
        if ((lat > 15 && lat < 35) &&
            ((lon > -120 && lon < -30) || (lon > 30 && lon < 120)))
            return "DST"; // Desert

        // Steppe (semi-arid grasslands) - improved range
        if ((lat >= 35 && lat < 50) &&
            ((lon > -120 && lon < -50) || (lon > 30 && lon < 140)))
            return "STP"; // Steppe

        // Grasslands and forests (general rule)
        if (lat >= 50 && lat < 66.5)
            return elevation < 500 ? "GRS" : "FOR"; // Grassland or Forest

            // Coastal regions (expanded and improved check with more flexibility for geographic features)
            if (((Math.Abs(lon) < 40 || Math.Abs(lon) > 140) ||  // Longitudes for Atlantic, Pacific coasts, and more
                (lat > 30 && lat < 40 && (lon > -80 && lon < -60)) || // East Coast USA (e.g., Florida, Carolinas)
                (lat > 36 && lat < 45 && (lon > 10 && lon < 20)) ||  // Mediterranean Coast (southern Europe)
                (lat > -30 && lat < -20 && (lon > -60 && lon < -50)) || // South America (Argentine coast)
                (lat > 50 && lat < 60 && (lon > -5 && lon < 5)) ||   // North Sea Coast (UK, Norway)
                (lat > -70 && lat < -60 && (lon > -60 && lon < -50)) || // Southern Ocean Coast (Antarctica, South Pole)
                (lat > -35 && lat < -25 && (lon > 140 && lon < 155)) || // Australia (Great Barrier Reef, Queensland Coast)
                (lat > -10 && lat < 10 && (lon > 110 && lon < 120)) || // Southeast Asia (Indonesia, Bali)
                (lat > 4 && lat < 12 && (lon > 122 && lon < 128)) ||  // Philippines Coast
                (lat > 4 && lat < 15 && (lon > -10 && lon < 5)) ||    // West Africa Coast (Senegal, Ghana, Ivory Coast)
                (lat > 25 && lat < 30 && (lon > -85 && lon < -80)) || // Gulf of Mexico Coast (USA, Mexico)
                (lat > 10 && lat < 20 && (lon > 90 && lon < 100)) ||  // Bay of Bengal Coast (Bangladesh, India)
                (lat > 12 && lat < 22 && (lon > -77 && lon < -60)) || // Caribbean Coast (Central America, islands)
                (lat > 24 && lat < 30 && (lon > 127 && lon < 135)) || // Southern Japan (Kyushu, Okinawa)
                (lat > -46 && lat < -34 && (lon > 166 && lon < 178)) || // New Zealand Coast (South and North Islands)
                (lat > 12 && lat < 15 && (lon > 40 && lon < 55)) ||  // East Coast of Africa (Somalia, Kenya, Tanzania)
                (lat > -10 && lat < 0 && (lon > 40 && lon < 60)) ||   // Madagascar Coast
                (lat > -5 && lat < 5 && (lon > -75 && lon < -65)) ||  // Pacific Coast of South America (Ecuador, Peru)
                (lat > 55 && lat < 65 && (lon > -170 && lon < -130)) || // Alaska Coast (USA, Pacific Coast)
                (lat > 60 && lat < 70 && (lon > -5 && lon < 20)) ||   // Norwegian Fjords
                (lat > 63 && lat < 66 && (lon > -25 && lon < -10)) || // Iceland Coast
                (lat > 15 && lat < 25 && (lon > -85 && lon < -75)) || // Cuba and Caribbean Islands
                (lat > 20 && lat < 30 && (lon > -70 && lon < -60)) || // Bahamas and surrounding Caribbean Islands
                (lat > 45 && lat < 50 && (lon > -125 && lon < -115))) // Pacific Coast of Canada (British Columbia)
                && lat > -60 && lat < 80 && elevation < 300)  // General latitude and elevation check
                return "CST"; // Coastal (Allowing higher elevation for fjords, cliffs, and steep coastlines)

        // Rivers
        if (
            // Major rivers by coordinates (Approximate ranges)
            (lat > 60 && lat < 70 && lon > 10 && lon < 30) ||  // Volga River (Russia)
            (lat > -34 && lat < -30 && lon > 146 && lon < 149) || // Murray River (Australia)
            (lat > 20 && lat < 30 && lon > 90 && lon < 100) ||  // Ganges River (India)
            (lat > 10 && lat < 20 && lon > -60 && lon < -40) || // Amazon River (South America)
            (lat > 30 && lat < 40 && lon > -95 && lon < -85) || // Mississippi River (USA)
            (lat > 30 && lat < 40 && lon > 30 && lon < 35) ||   // Nile River (Africa)
            (lat > 4 && lat < 10 && lon > 100 && lon < 110) ||  // Mekong River (Southeast Asia)
            (lat > 12 && lat < 18 && lon > 45 && lon < 55) ||  // Zambezi River (Africa)
            (lat > 36 && lat < 45 && lon > -90 && lon < -80) || // Missouri River (USA)
            (lat > 5 && lat < 10 && lon > 120 && lon < 130) ||  // Chao Phraya River (Thailand)
            (lat > 10 && lat < 15 && lon > 72 && lon < 80)      // Godavari River (India)
            )
            return "RIV"; // River`

        // Oceans (using known oceanic areas by latitude and longitude ranges)
        if (
            // Southern Ocean
            (lat > -90 && lat < -60 && lon > -180 && lon < 180) || 
            // Arctic Ocean
            (lat > 60 && lat < 90 && lon > -180 && lon < 180) ||  
            // South Pacific Ocean
            (lat > -60 && lat < 0 && lon > -180 && lon < -30) ||  
            // North Pacific Ocean
            (lat > 0 && lat < 60 && lon > -180 && lon < -30) ||   
            // Indian Ocean
            (lat > 0 && lat < 60 && lon > 30 && lon < 180) ||     
            // Southern Indian Ocean
            (lat > -60 && lat < 0 && lon > 30 && lon < 180) ||    
            // Southern Atlantic Ocean
            (lat > -60 && lat < 0 && lon > -180 && lon < -30) ||  
            // North Atlantic Ocean
            (lat > 0 && lat < 60 && lon > -30 && lon < 30)
        )
            return "OCN"; // Ocean

        // Seas (specific seas based on global locations)
        if (
            // Caribbean Sea
            (lat > 10 && lat < 60 && lon > -60 && lon < -30) ||   
            // Gulf of Mexico
            (lat > 30 && lat < 40 && lon > -90 && lon < -85) ||   
            // South China Sea
            (lat > -15 && lat < 15 && lon > 120 && lon < 130) ||  
            // Coral Sea
            (lat > -15 && lat < 15 && lon > 100 && lon < 120) ||     
            // Arabian Sea
            (lat > 25 && lat < 45 && lon > 50 && lon < 70) ||     
            // Red Sea
            (lat > 10 && lat < 20 && lon > 40 && lon < 60) ||    
            // Philippine Sea
            (lat > 20 && lat < 30 && lon > 120 && lon < 140) ||   
            // Bay of Bengal
            (lat > 0 && lat < 20 && lon > 60 && lon < 75) ||      
            // North Sea
            (lat > 40 && lat < 50 && lon > 5 && lon < 15) ||      
            // Arafura Sea
            (lat > -20 && lat < -10 && lon > 110 && lon < 120) || 
            // Sulu Sea
            (lat > 15 && lat < 25 && lon > 100 && lon < 120) ||  
            // Baltic Sea
            (lat > 55 && lat < 65 && lon > 10 && lon < 30) ||
            // Tasman Sea
            (lat > -45 && lat < -30 && lon > 140 && lon < 160) ||
            // South Sea (South Pacific Ocean region)
            (lat > -60 && lat < 0 && lon > 140 && lon < 180)
        )
            return "SEA"; // Sea


        /// Delta regions (expanded coverage with more global deltas)
        if (((lat > 10 && lat < 30) && (lon > 80 && lon < 100)) ||  // Ganges-Brahmaputra Delta
            ((lat > -5 && lat < 5) && (lon > -60 && lon < -40)) ||  // Amazon Delta
            ((lat > 25 && lat < 35) && (lon > 30 && lon < 35)) ||   // Nile Delta
            ((lat > 28 && lat < 33) && (lon > -95 && lon < -88)) || // Mississippi Delta
            ((lat > 8 && lat < 13) && (lon > 104 && lon < 106)) || // Mekong Delta (Vietnam and Cambodia)
            ((lat > 23 && lat < 30) && (lon > 67 && lon < 75)) ||  // Indus Delta (Pakistan)
            ((lat > 51 && lat < 53) && (lon > 3 && lon < 7)) ||    // Rhine-Meuse Delta (Netherlands, Belgium)
            ((lat > 42 && lat < 46) && (lon > 46 && lon < 50)) ||  // Volga Delta (Russia)
            ((lat > 30 && lat < 35) && (lon > 120 && lon < 125)) ||// Yangtze River Delta (China)
            ((lat > 44 && lat < 46) && (lon > 28 && lon < 30)) ||  // Danube Delta (Romania, Ukraine)
            ((lat > 20 && lat < 22) && (lon > 70 && lon < 73)) ||  // Gulf of Khambhat / Sabarmati Delta (India)
            ((lat > -34 && lat < -30) && (lon > -59 && lon < -56)) || // Parana Delta (Argentina)
            ((lat > -37 && lat < -34) && (lon > 141 && lon < 149)) || // Murray-Darling Delta (Australia)
            ((lat > 71 && lat < 73) && (lon > 125 && lon < 140)) || // Lena Delta (Russia)
            ((lat > -20 && lat < -16) && (lon > 34 && lon < 40)) || // Zambezi Delta (Mozambique)
            ((lat > -35 && lat < -33) && (lon > -58 && lon < -57)) || // Río de la Plata Delta (Argentina/Uruguay)
            ((lat > 58 && lat < 60) && (lon > 75 && lon < 80)) ||  // Ob-Irtysh Delta (Russia)
            ((lat > 29 && lat < 30) && (lon > 48 && lon < 50)) ||  // Shatt al-Arab Delta (Iraq/Iran)
            ((lat > 10 && lat < 15) && (lon > 92 && lon < 95)) ||  // Irrawaddy Delta (Myanmar)
            ((lat > 4 && lat < 10) && (lon > 5 && lon < 15)) ||    // Niger Delta (Nigeria)
            ((lat > 39 && lat < 42) && (lon > -95 && lon < -90)) || // Missouri River Delta (USA)
            ((lat > 21 && lat < 22) && (lon > 99 && lon < 103)) || // Red River Delta (Vietnam)
            ((lat > -18 && lat < -15) && (lon > 21 && lon < 23)) || // Okavango Delta (Botswana)
            ((lat > 10 && lat < 14) && (lon > 98 && lon < 101)))   // Chao Phraya Delta (Thailand)
            return "DLT"; // Delta

        // Volcanic regions (expanded to more areas)
        if (
            // Indonesia (Ring of Fire)
            ((lat > -10 && lat < 10) && (lon > 90 && lon < 150)) ||

            // Japan & Philippines (Ring of Fire)
            ((lat > 25 && lat < 45) && (lon > 120 && lon < 150)) ||

            // Hawaii (Hotspot volcanism)
            ((lat > 15 && lat < 25) && (lon > -180 && lon < -150)) ||

            // Iceland (Mid-Atlantic Ridge)
            ((lat > 60 && lat < 67) && (lon > -30 && lon < -10)) ||

            // Italy (Vesuvius, Etna, Stromboli)
            ((lat > 35 && lat < 45) && (lon > 10 && lon < 20)) ||

            // Central America (Guatemala, Nicaragua, Costa Rica)
            ((lat > 5 && lat < 20) && (lon > -95 && lon < -75)) ||

            // Andes volcanic belt (Ecuador, Chile, Peru)
            ((lat > -40 && lat < 5) && (lon > -80 && lon < -65)) ||

            // Kamchatka Peninsula & Russian Far East
            ((lat > 50 && lat < 65) && (lon > 155 && lon < 175)) ||

            // Aleutian Islands (Alaska)
            ((lat > 50 && lat < 55) && (lon > -180 && lon < -160)) ||

            // Papua New Guinea & Solomon Islands
            ((lat > -10 && lat < 0) && (lon > 140 && lon < 160)) ||

            // Tonga & Vanuatu (South Pacific Ring of Fire)
            ((lat > -25 && lat < -10) && (lon > 165 && lon < 180)) ||

            // East African Rift (Mount Nyiragongo, Mount Kilimanjaro, Ethiopian Rift)
            ((lat > -15 && lat < 15) && (lon > 25 && lon < 50)) ||

            // Yellowstone (USA Supervolcano)
            ((lat > 40 && lat < 45) && (lon > -115 && lon < -105)) ||

            // Canary Islands (Spain, Atlantic hotspot)
            ((lat > 25 && lat < 30) && (lon > -20 && lon < -10)) ||

            // Azores (Portugal, Mid-Atlantic Ridge volcanism)
            ((lat > 35 && lat < 40) && (lon > -30 && lon < -25)) ||

            // Ethiopian Highlands (Erta Ale & East African Rift)
            ((lat > 5 && lat < 15) && (lon > 35 && lon < 45)) ||

            // Réunion Island (Piton de la Fournaise, Indian Ocean Hotspot)
            ((lat > -25 && lat < -20) && (lon > 50 && lon < 60)) ||

            // Antarctica (Mount Erebus, Deception Island, other active volcanoes)
            ((lat > -80 && lat < -60) && (lon > -180 && lon < 180)) ||

            // Kuril Islands (Russia, Pacific Ring of Fire)
            ((lat > 43 && lat < 50) && (lon > 145 && lon < 156)) ||

            // New Zealand (Taupō Volcanic Zone, White Island, Mount Ruapehu)
            ((lat > -40 && lat < -30) && (lon > 170 && lon < 180))
        )
            return "VOL"; // Volcanic

        return "N_A"; // Default if no other match
    }

    public string DetermineClimateZone(double lat, double lon, double elevation)
    {
        // High-altitude areas (Tundra)
        if (elevation > 2500)
            return "ET";

        // Tropical climates
        if (lat >= -23.5 && lat <= 23.5)
            return elevation < 1000 ? "Af" : "As"; // Rainforest or Tropical Wet & Dry

        // Hot Deserts
        if ((lat > 15 && lat < 35) &&
            ((lon > -120 && lon < -30) || (lon > 30 && lon < 120) || (lon > -70 && lon < -50)))
            return "BWh"; // Hot Desert

        // Cold Deserts
        if ((lat > 35 && lat < 50) &&
            ((lon > 40 && lon < 120) || (lon > -120 && lon < -100)))
            return "BWk"; // Cold Desert

        // Temperate zones
        if (lat >= 35 && lat < 50)
        {
            if ((lon > -125 && lon < -60) || (lon > 0 && lon < 30))
                return "Cfb"; // Marine West Coast
            return "Cfa"; // Humid Subtropical
        }

        // Boreal/Cold climates
        if (lat >= 50 && lat < 66.5)
        {
            if (Math.Abs(lon) < 30)
                return "Cfc"; // Subpolar Oceanic
            if ((lon > 60 && lon < 150) || (lon > -130 && lon < -60)) // Siberia, Canada
                return "Dfd"; // Very Cold Subarctic
            return "Dfc"; // Subarctic
        }

        // Ice Cap for polar regions
        return "EF";
    }

    public double GetElevation(double lat, double lon)
    {
        var tempClimateZone = DetermineClimateZone(lat, lon, 0);
        var tempSurfaceType = DetermineSurfaceType(lat, lon, 0);

        // Skip elevation lookup for polar, coastal regions, certain climate zones, oceans, seas, and rivers
        if (tempSurfaceType == "POL" || tempSurfaceType == "CST" || 
            tempClimateZone == "Cfc" || tempClimateZone == "Cfb" ||
            tempSurfaceType == "OCN" || tempSurfaceType == "SEA")
        {
            return 0.0; // No elevation available
        }

        // Approximation based on latitude and longitude for each continent
        if (lat >= -60 && lat <= 60) // Likely to be in temperate/tropical zones
        {
            // South America
            if (lon >= -80 && lon <= -60) 
            {
                return 2500; // Approximate elevation of the Andes
            }
            // Asia
            else if (lon >= 60 && lon <= 120) 
            {
                return 4000; // Approximate elevation of the Himalayas (Tibetan Plateau)
            }
            // Australia
            else if (lon >= 120 && lon <= 160) 
            {
                return 300; // Low elevation desert in the Australian Outback
            }
            // Africa
            else if (lon >= -10 && lon <= 10) 
            {
                return 500; // Approximate elevation of Central Africa's plains
            }
            // North America
            else if (lon >= -125 && lon <= -60)
            {
                return 1000; // Rocky Mountains (North America)
            }
            // Europe
            else if (lon >= -10 && lon <= 30)
            {
                return 500; // Approximate elevation of European plains (Alps are higher)
            }
        }

        // Northern Hemisphere (Arctic regions)
        if (lat > 60) 
        {
            return 0.0; // Polar regions, no elevation data
        }

        // Southern Hemisphere (Antarctic regions)
        if (lat < -60) 
        {
            return 0.0; // Polar regions, no elevation data
        }

        // Default return value for any other case
        return 1000; // General highland area assumption
    }
    

    public string DetermineTimezone(double latitude, double longitude)
    {
        try
        {
            var timeZoneId = TimeZoneLookup.GetTimeZone(latitude, longitude).Result;
            if (string.IsNullOrEmpty(timeZoneId))
                return "N_A";

            try
            {
                var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                var offset = timeZoneInfo.BaseUtcOffset;

                // Format the offset with hours and minutes (e.g., UTC+12:00 or UTC-05:00)
                var sign = offset.TotalHours >= 0 ? "+" : "-";
                return $"UTC{sign}{Math.Abs(offset.Hours):D2}:{Math.Abs(offset.Minutes):D2}";
            }
            catch (TimeZoneNotFoundException)
            {
                return "N_A";
            }
        }
        catch (Exception)
        {
            return "N_A";
        }
    }

    public double DetermineDepth(double lat, double lon, string surfaceType)
    {
        var random = new Random();

        return surfaceType switch
        {
            // Ocean depths range from 200m (continental shelf) to 11,000m (Mariana Trench)
            "OCN" => Math.Round(random.Next(200, 11000) + random.NextDouble(), 2),
            // Sea depths range from 10m (shallow coastal) to 3000m (deepest seas)
            "SEA" => Math.Round(random.Next(10, 3000) + random.NextDouble(), 2),
            // Deltas are generally shallow, ranging from 0m to 50m
            "DLT" => Math.Round(random.Next(0, 50) + random.NextDouble(), 2),
            // Rivers are mostly shallow, usually 1m to 50m depth
            "RIV" => Math.Round(random.Next(1, 50) + random.NextDouble(), 2),
            // Coastal areas range from 0m (beach) to 200m (continental shelf)
            "CST" => Math.Round(random.Next(0, 200) + random.NextDouble(), 2),
            // Default for land, assuming no depth
            _ => 0,
        };
    }
}
