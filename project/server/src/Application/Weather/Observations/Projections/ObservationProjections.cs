// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Weather.Observations.Models;

namespace server.src.Application.Weather.Observations.Projections;

public static class ObservationProjections
{
    public static Expression<Func<Observation, Observation>> GetObservationsProjection()
    {
        return f => new Observation
        {
            Id = f.Id,
            Timestamp = f.Timestamp,
            TemperatureC = f.TemperatureC,
            Humidity = f.Humidity,
            MoonPhaseId = f.MoonPhaseId,
            MoonPhase = f.MoonPhase,
            LocationId = f.LocationId,
            Location = f.Location,
        };
    }
}