using TimeScout.Domain.Entities;

namespace TimeScout.Domain.Interfaces;

public interface ITagRepository : IRepository<Tag>
{
    Task<Tag?> GetTagByIdAsync(int id);
    Task<IEnumerable<Tag>> GetAllTagsAsync();
}
