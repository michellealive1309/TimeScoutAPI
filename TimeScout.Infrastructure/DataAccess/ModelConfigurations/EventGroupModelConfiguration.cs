using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeScout.Domain.Entities;

namespace TimeScout.Infrastructure.DataAccess.ModelConfigurations;

public class EventGroupModelConfiguration : IEntityTypeConfiguration<EventGroup>
{
    public void Configure(EntityTypeBuilder<EventGroup> builder)
    {
        builder.ToTable("event_groups");
        builder.HasKey(eg => eg.Id);

        builder.HasQueryFilter(eg => !eg.IsDeleted);

        builder.HasMany(eg => eg.Events)
               .WithOne(e => e.EventGroup)
               .HasForeignKey(e => e.EventGroupId)
               .IsRequired(false);
        builder.HasMany(eg => eg.Members)
               .WithMany(u => u.EventGroups)
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
