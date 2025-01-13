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

        builder.HasQueryFilter(eg => !eg.IsDeleted);

        builder.HasMany<Event>()
               .WithOne(e => e.EventGroup)
               .HasForeignKey(e => e.EventGroupId);
        builder.HasMany<User>()
               .WithMany()
               .UsingEntity<EventGroupMember>(
                    j => j.HasOne(egm => egm.User)
                         .WithMany()
                         .HasForeignKey(egm => egm.UserId),
                    j => j.HasOne(egm => egm.EventGroup)
                         .WithMany()
                         .HasForeignKey(egm => egm.EventGroupId),
                    j => j.ToTable("event_group_members")
               );
    }
}
