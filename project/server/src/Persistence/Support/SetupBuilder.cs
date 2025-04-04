// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Support.ChangeLogs;
using server.src.Persistence.Support.Changes;
using server.src.Persistence.Support.ChangeTypes;
using server.src.Persistence.Support.FAQCategories;
using server.src.Persistence.Support.FAQs;

namespace server.src.Persistence.Support;

public static class SetupBuilder
{
    private readonly static string _supportSchema = "supprort";

    public static void SetupSupport(this ModelBuilder modelBuilder)
    {
        #region Configuration
        modelBuilder.ApplyConfiguration(new ChangeLogConfiguration("ChangeLogs", _supportSchema));
        modelBuilder.ApplyConfiguration(new ChangeConfiguration("Changes", _supportSchema));
        modelBuilder.ApplyConfiguration(new ChangeTypeConfiguration("ChangeTypes", _supportSchema));
        modelBuilder.ApplyConfiguration(new FAQCategoryConfiguration("FAQCategories", _supportSchema));
        modelBuilder.ApplyConfiguration(new FAQConfiguration("FAQs", _supportSchema));
        #endregion

        #region Indexes
        modelBuilder.ApplyConfiguration(new ChangeLogIndexes());
        modelBuilder.ApplyConfiguration(new ChangeIndexes());
        modelBuilder.ApplyConfiguration(new ChangeTypeIndexes());
        modelBuilder.ApplyConfiguration(new FAQCategoryIndexes());
        modelBuilder.ApplyConfiguration(new FAQIndexes());
        #endregion
    }
}