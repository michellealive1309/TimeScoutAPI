using TimeScout.Domain.Entities;

namespace TimeScout.Domain.Interfaces;

public interface ITagRepository : IRepository<Tag>
{
    Task<Tag?> GetTagByIdAsync(int id, int userId);
    Task<IEnumerable<Tag>> GetAllTagsAsync(int userId);
}
