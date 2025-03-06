// packages
using System.Net;
using Microsoft.EntityFrameworkCore;

// source
using server.src.Application.Common.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Persistence.Common.Contexts;

namespace server.src.Application.Data.Commands;

public record ClearDataCommand : IRequest<Response<string>>;

public class ClearDataHandler : IRequestHandler<ClearDataCommand, Response<string>>
{
    private readonly DataContext _dataContext;
    private readonly ArchiveContext _archiveContext;
    
    public ClearDataHandler(DataContext dataContext, ArchiveContext archiveContext)
    {
        _dataContext = dataContext;
        _archiveContext = archiveContext;
    }

    public async Task<Response<string>> Handle(ClearDataCommand request, CancellationToken token = default)
    {
        // Clearing main database
        var dbSetContainers = new object[]
        {
            _dataContext.AuthDbSets,
            _dataContext.GeographyDbSets.AdministrativeDbSets,
            _dataContext.GeographyDbSets.NaturalDbSets,
            _dataContext.MetricsDbSets,
            _dataContext.WeatherDbSets
        };

        foreach (var dbSetContainer in dbSetContainers)
        {
            // Get all DbSet properties dynamically from each DbSet container
            var dataDbSets = dbSetContainer.GetType()
                .GetProperties()
                .Where(p => p.PropertyType.IsGenericType 
                        && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .ToList();

            foreach (var dbSetProperty in dataDbSets)
            {
                // Get DbSet instance
                var dbSet = dbSetProperty.GetValue(dbSetContainer);

                if (dbSet is IQueryable<object> queryableDbSet)
                {
                    // Remove all records in the DbSet
                    _dataContext.RemoveRange(queryableDbSet);
                }
            }
        }

        // Save changes to delete data from the main database
        var resultData = await _dataContext.SaveChangesAsync(token) > 0;

        if (!resultData)
            return new Response<string>()
                .WithMessage("Error in clearing data")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data clearing failed!");

        // Clearing archive database
        var archiveDbSetContainers = new object[]
        {
            _archiveContext.AuthDbSets,
            _archiveContext.GeographyDbSets.AdministrativeDbSets,
            _archiveContext.GeographyDbSets.NaturalDbSets,
            _archiveContext.MetricsDbSets,
            _archiveContext.WeatherDbSets
        };

        foreach (var dbSetContainer in archiveDbSetContainers)
        {
            // Get all DbSet properties dynamically from each DbSet container in archive
            var archiveDbSets = dbSetContainer.GetType()
                .GetProperties()
                .Where(p => p.PropertyType.IsGenericType 
                        && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .ToList();

            foreach (var dbSetProperty in archiveDbSets)
            {
                // Get DbSet instance
                var dbSet = dbSetProperty.GetValue(dbSetContainer);

                if (dbSet is IQueryable<object> queryableDbSet)
                {
                    // Remove all records in the DbSet
                    _archiveContext.RemoveRange(queryableDbSet);
                }
            }
        }

        // Save changes to delete data from the archive database
        var resultArchive = await _archiveContext.SaveChangesAsync(token) > 0;

        if (!resultArchive)
            return new Response<string>()
                .WithMessage("Error in clearing archive")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Archive clearing failed!");

        return new Response<string>()
            .WithMessage("Success in clearing data")
            .WithSuccess(true)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithData("Data and archive deletion was successful!");
    }
}