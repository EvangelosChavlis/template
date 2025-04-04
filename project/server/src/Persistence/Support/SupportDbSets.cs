// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Domain.Support.ChangeLogs.Models;
using server.src.Domain.Support.Changes.Models;
using server.src.Domain.Support.ChangeTypes.Models;
using server.src.Domain.Support.FAQCategories.Models;
using server.src.Domain.Support.FAQs.Models;

namespace server.src.Persistence.Support;

public class SupportDbSets
{
    public DbSet<ChangeLog> ChangeLogs { get; private set; }
    public DbSet<Change> Changes { get; private set; }
    public DbSet<ChangeType> ChangeTypes { get; private set; }
    public DbSet<FAQCategory> FAQCategories { get; private set; }
    public DbSet<FAQ> FAQs { get; private set; }

    public SupportDbSets(DbContext context)
    {
        ChangeLogs = context.Set<ChangeLog>();
        Changes = context.Set<Change>();
        ChangeTypes = context.Set<ChangeType>();
        FAQCategories = context.Set<FAQCategory>();
        FAQs = context.Set<FAQ>();
    }
}