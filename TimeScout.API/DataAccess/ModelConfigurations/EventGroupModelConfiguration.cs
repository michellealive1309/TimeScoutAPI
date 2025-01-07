using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeScout.API.Models;

namespace TimeScout.API.DataAccess.ModelConfigurations;

public class EventGroupModelConfiguration : IEntityTypeConfiguration<EventGroup>
{
    public void Configure(EntityTypeBuilder<EventGroup> builder)
    {
        builder.ToTable("event_groups");
        builder.HasKey(eg => eg.Id);

        builder.HasMany<Event>()
               .WithOne(e => e.EventGroup)
               .HasForeignKey(e => e.EventGroupId);
        builder.HasMany<EventGroupMember>()
               .WithOne()
               .HasForeignKey(egm => egm.EventGroupId);
    }
}
