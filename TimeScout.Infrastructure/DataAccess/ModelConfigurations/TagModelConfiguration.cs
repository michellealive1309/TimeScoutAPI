using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeScout.Domain.Entities;

namespace TimeScout.Infrastructure.DataAccess.ModelConfigurations;

public class TagModelConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tags");
        builder.HasKey(t => t.Id);

        builder.HasMany<Event>()
            .WithOne(e => e.Tag)
            .HasForeignKey(e => e.TagId)
            .IsRequired(false);
    }
}
