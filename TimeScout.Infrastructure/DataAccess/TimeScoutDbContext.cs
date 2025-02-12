using TimeScout.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TimeScout.Infrastructure.DataAccess.ModelConfigurations;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TimeScout.Infrastructure.Provider;

namespace TimeScout.Infrastructure.DataAccess;

public class TimeScoutDbContext : DbContext
{
    private readonly ICurrentUserProvider _currentUser;
    private int? UserId => _currentUser.GetUserId();

    public TimeScoutDbContext(
        DbContextOptions<TimeScoutDbContext> options,
        ICurrentUserProvider currentUser
    ) : base(options)
    {
        _currentUser = currentUser;
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<EventGroup> EventGroups { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EventGroupModelConfiguration());
        modelBuilder.ApplyConfiguration(new EventModelConfiguration());
        modelBuilder.ApplyConfiguration(new UserModelConfiguration());
        modelBuilder.ApplyConfiguration(new TagModelConfiguration());

        modelBuilder.Entity<Event>().HasQueryFilter(e => e.UserId == UserId);
        modelBuilder.Entity<Tag>().HasQueryFilter(e => e.UserId == UserId);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity);

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;
            switch (entry.State)
            {
                case EntityState.Added:
                    entity.Created = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entity.Modified = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entity.Modified = DateTime.UtcNow;
                    entity.IsDeleted = true;
                    break;
            }
        }

        ChangeTracker.DetectChanges();
        return base.SaveChangesAsync(cancellationToken);
    }
}
