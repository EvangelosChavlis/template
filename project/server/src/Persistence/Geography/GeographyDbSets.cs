// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Geography.Administrative;
using server.src.Persistence.Geography.Natural;

namespace server.src.Persistence.Geography;

public class GeographyDbSets
{
    public AdministrativeDbSets AdministrativeDbSets { get; private set; }
    public NaturalDbSets NaturalDbSets { get; private set; }

    public GeographyDbSets(DbContext context)
    {
        AdministrativeDbSets = new AdministrativeDbSets(context);
        NaturalDbSets = new NaturalDbSets(context);
    }
}