using TimeScout.Domain.Entities;

namespace TimeScout.Application.Interfaces;

public interface ITagService
{
    Task<Tag?> CreateTagAsync(Tag newTag);
    Task<bool> DeleteTagAsync(int id, int userId);
    Task<Tag?> GetTagByIdAsync(int id);
    Task<IEnumerable<Tag>> GetAllTagsAsync();
    Task<Tag?> UpdateTagAsync(Tag updateTag);
}
