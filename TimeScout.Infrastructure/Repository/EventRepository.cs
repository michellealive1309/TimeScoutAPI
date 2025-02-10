using Microsoft.EntityFrameworkCore;
using TimeScout.Infrastructure.DataAccess;
using TimeScout.Domain.Entities;
using TimeScout.Domain.Interfaces;

namespace TimeScout.Infrastructure.Repository;

public class EventRepository : Repository<Event>, IEventRepository
{
    private readonly TimeScoutDbContext _context;
    public EventRepository(TimeScoutDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<Event?> GetEventByIdAsync(int id, int userId)
    {
        return _context.Events
            .Where(e => e.UserId == userId)
            .Include(e => e.EventGroup)
            .Include(e => e.Tag)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Event>> GetEventsByDateRangeAsync(DateOnly start, DateOnly end, int userId)
    {
        return await _context.Events
            .AsNoTracking()
            .Where(e => e.StartDate >= start)
            .Where(e => e.EndDate <= end)
            .Where(e => e.UserId == userId)
            .Include(e => e.EventGroup)
            .Include(e => e.Tag)
            .ToListAsync();
    }
}
