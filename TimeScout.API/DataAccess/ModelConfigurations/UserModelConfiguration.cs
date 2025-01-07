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

        builder.HasIndex(u => u.Email)
               .IsUnique();

        builder.HasMany<Event>()
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);
        builder.HasMany<EventGroupMember>()
                .WithOne(egm => egm.User)
                .HasForeignKey(egm => egm.UserId);
    }
}
