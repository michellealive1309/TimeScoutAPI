using System;
using TimeScout.API.Models;
using Microsoft.EntityFrameworkCore;
using TimeScout.API.DataAccess.ModelConfigurations;
using TimeScout.API.Entity;

namespace TimeScout.API.DataAccess;

public class TimeScoutDbContext : DbContext
{
    public TimeScoutDbContext(DbContextOptions<TimeScoutDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<EventGroup> Groups { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EventGroupModelConfiguration());
        modelBuilder.ApplyConfiguration(new EventModelConfiguration());
        modelBuilder.ApplyConfiguration(new UserModelConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;
            switch (entry.State)
            {
                case EntityState.Added:
                    entity.Created = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entity.IsDeleted = true;
                    break;
            }

            entity.Modified = DateTime.UtcNow;
        }

        ChangeTracker.DetectChanges();
        return base.SaveChangesAsync(cancellationToken);
    }
}
