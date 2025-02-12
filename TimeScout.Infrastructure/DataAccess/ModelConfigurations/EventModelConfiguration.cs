using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeScout.Domain.Entities;

namespace TimeScout.Infrastructure.DataAccess.ModelConfigurations;

public class EventModelConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("events");
        builder.HasKey(e => e.Id);

        builder.HasQueryFilter(e => !e.IsDeleted);
        
        builder.HasIndex(e => e.EventGroupId);
        builder.HasIndex(e => e.UserId);

        builder.HasOne(e => e.EventGroup)
               .WithMany(eg => eg.Events)
               .HasForeignKey(e => e.EventGroupId)
               .IsRequired(false);
        builder.HasOne(e => e.Tag)
               .WithMany()
               .HasForeignKey(e => e.TagId)
               .IsRequired(false);
        builder.HasOne(e => e.User)
               .WithMany()
               .HasForeignKey(e => e.UserId);
    }
}
