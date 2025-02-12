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

    public async Task<IEnumerable<Tag>> GetAllTagsAsync()
    {
        return await _context.Tags.AsNoTracking().ToListAsync();
    }

    public async Task<Tag?> GetTagByIdAsync(int id)
    {
        
        return await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
    }
}
