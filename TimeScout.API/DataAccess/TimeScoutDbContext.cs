using System;
using TimeScout.API.Models;
using Microsoft.EntityFrameworkCore;
using TimeScout.API.DataAccess.ModelConfigurations;

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
}
