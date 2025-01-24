using System;
using Microsoft.EntityFrameworkCore;
using TimeScout.API.DataAccess;
using TimeScout.API.Models;

namespace TimeScout.API.Repository;

public class EventGroupRepository : Repository<EventGroup>, IEventGroupRepository
{
    private readonly TimeScoutDbContext _context;
    public EventGroupRepository(TimeScoutDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EventGroup>> GetAllEventGroupAsync(int userId)
    {
        return await _context.EventGroups
            .AsNoTracking()
            .Where(eg => eg.Members!.Any(m => m.Id == userId))
            .Include(eg => eg.Members)
            .ToListAsync();
    }

    public Task<EventGroup?> GetEventGroupByIdAsync(int id, int userId)
    {
        return _context.EventGroups
            .AsNoTracking()
            .Where(eg => eg.Members!.Any(m => m.Id == userId))
            .Include(eg => eg.Members)
            .FirstOrDefaultAsync(eg => eg.Id == id);
    }
    
    public async Task<IEnumerable<User>> GetMembersAsync(IEnumerable<User> members)
    {
        var memberIds = members.Select(m => m.Id);

        return await _context.Users
            .Where(u => memberIds.Contains(u.Id))
            .ToListAsync();
    }
}
