using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeScout.API.Models;

namespace TimeScout.API.DataAccess.ModelConfigurations;

public class EventGroupMemberModelConfiguration : IEntityTypeConfiguration<EventGroupMember>
{
    public void Configure(EntityTypeBuilder<EventGroupMember> builder)
    {
        builder.ToTable("event_group_members");
        builder.HasNoKey();
        
        builder.HasIndex(egm => egm.EventGroupId);
        builder.HasIndex(egm => egm.UserId);

        builder.HasOne(egm => egm.EventGroup)
               .WithMany()
               .HasForeignKey(egm => egm.EventGroupId)
               .IsRequired();
        builder.HasOne(egm => egm.User)
               .WithMany()
               .HasForeignKey(egm => egm.UserId)
               .IsRequired();
    }
}
