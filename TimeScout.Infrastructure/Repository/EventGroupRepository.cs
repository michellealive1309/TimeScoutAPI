using Microsoft.EntityFrameworkCore;
using TimeScout.Infrastructure.DataAccess;
using TimeScout.Domain.Entities;
using TimeScout.Domain.Interfaces;

namespace TimeScout.Infrastructure.Repository;

public class EventGroupRepository : Repository<EventGroup>, IEventGroupRepository
{
    private readonly TimeScoutDbContext _context;
    public EventGroupRepository(TimeScoutDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<EventGroup?> FindEventGroupWithMemberByIdAsync(int id)
    {
        return _context.EventGroups
            .Include(eg => eg.Members)
            .FirstOrDefaultAsync(eg => eg.Id == id);
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
