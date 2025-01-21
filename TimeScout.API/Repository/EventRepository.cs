using System;
using Microsoft.EntityFrameworkCore;
using TimeScout.API.DataAccess;
using TimeScout.API.Models;

namespace TimeScout.API.Repository;

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
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}
