using Microsoft.EntityFrameworkCore;
using TimeScout.Infrastructure.DataAccess;
using TimeScout.Domain.Entities;
using TimeScout.Domain.Interfaces;

namespace TimeScout.Infrastructure.Repository;

public class TagRepository : Repository<Tag>, ITagRepository
{
    private readonly TimeScoutDbContext _context;
    public TagRepository(TimeScoutDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tag>> GetAllTagsAsync(int userId)
    {
        return await _context.Tags.AsNoTracking()
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<Tag?> GetTagByIdAsync(int id, int userId)
    {
        
        return await _context.Tags
            .Where(t => t.UserId == userId)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}
