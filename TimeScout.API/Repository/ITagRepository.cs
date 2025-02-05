using TimeScout.API.Models;

namespace TimeScout.API.Repository;

public interface ITagRepository : IRepository<Tag>
{
    Task<Tag?> GetTagByIdAsync(int id, int userId);
    Task<IEnumerable<Tag>> GetAllTagsAsync(int userId);
}
