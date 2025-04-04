// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Support.FAQs.Extensions;
using server.src.Domain.Support.FAQs.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Support.FAQs;

public class FAQConfiguration : IEntityTypeConfiguration<FAQ>
{
    private readonly string _tableName;
    private readonly string _schema;

    public FAQConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<FAQ> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(f => f.Title)
            .IsRequired()
            .HasMaxLength(FAQSettings.TitleLength);

        builder.Property(f => f.Question)
            .IsRequired()
            .HasMaxLength(FAQSettings.QuestionLength);

        builder.Property(f => f.Answer)
            .IsRequired()
            .HasMaxLength(FAQSettings.AnswerLength);

        builder.Property(f => f.ViewCount)
            .IsRequired()
            .HasDefaultValue(FAQSettings.ViewCountDefaultValue);

        builder.Property(f => f.IsActive)
            .IsRequired();

        builder.HasOne(f => f.FAQCategory)
            .WithMany(fc => fc.FAQs)
            .HasForeignKey(f => f.FAQCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}