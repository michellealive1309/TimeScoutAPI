using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeScout.API.Models;

namespace TimeScout.API.DataAccess.ModelConfigurations;

public class EventModelConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("events");
        builder.HasKey(e => e.Id);
        
        builder.HasIndex(e => e.EventGroupId);
        builder.HasIndex(e => e.UserId);

        builder.HasOne(e => e.EventGroup)
               .WithMany(eg => eg.Events)
               .HasForeignKey(e => e.EventGroupId);
        builder.HasOne(e => e.User)
               .WithMany()
               .HasForeignKey(e => e.UserId);
    }
}
