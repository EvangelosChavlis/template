// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Weather.Collections.Observations.Models;

namespace server.src.Application.Weather.Collections.Observations.Projections;

public static class ObservationProjections
{
    public static Expression<Func<Observation, Observation>> GetObservationsProjection()
    {
        return o => new Observation
        {
            Id = o.Id,
            Timestamp = o.Timestamp,
            TemperatureC = o.TemperatureC,
            Humidity = o.Humidity,
            MoonPhaseId = o.MoonPhaseId,
            MoonPhase = o.MoonPhase,
            StationId = o.StationId,
            Station = o.Station,
        };
    }
}