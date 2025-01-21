using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeScout.API.Models;

namespace TimeScout.API.DataAccess.ModelConfigurations;

public class UserModelConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);

        builder.HasQueryFilter(u => !u.IsDeleted);

        builder.HasIndex(u => u.Email)
               .IsUnique();

        builder.HasMany<Event>()
               .WithOne(e => e.User)
               .HasForeignKey(e => e.UserId);
        builder.HasMany(u => u.EventGroups)
               .WithMany(eg => eg.Members)
               .UsingEntity<EventGroupMember>(
                    j => j.HasOne(egm => egm.EventGroup)
                        .WithMany()
                        .HasForeignKey(egm => egm.EventGroupId),
                    j => j.HasOne(egm => egm.User)
                        .WithMany()
                        .HasForeignKey(egm => egm.UserId),
                    j => j.ToTable("event_group_members")
               );
    }
}
