// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Geography.Administrative;
using server.src.Persistence.Geography.Natural;

namespace server.src.Persistence.Geography;

public static class SetupBuilder
{
    public static void SetupGeography(this ModelBuilder modelBuilder)
    {
        modelBuilder.SetupAdministrative();
        modelBuilder.SetupNatural();
    }
}