using TimeScout.Domain.Entities;
using TimeScout.Domain.Interfaces;
using TimeScout.Application.Interfaces;

namespace TimeScout.Application.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<Tag?> CreateTagAsync(Tag newTag)
    {
        if (newTag.UserId == 0)
        {
            return null;
        }

        await _tagRepository.AddAsync(newTag);

        return newTag;
    }

    public async Task<bool> DeleteTagAsync(int id, int userId)
    {
        var toDeleteTag = await _tagRepository.FindAsync(id);

        if (toDeleteTag == null || toDeleteTag.UserId != userId)
        {
            return false;
        }

        await _tagRepository.RemoveAsync(toDeleteTag);

        return true;
    }

    public Task<IEnumerable<Tag>> GetAllTagsAsync()
    {
        return _tagRepository.GetAllTagsAsync();
    }

    public Task<Tag?> GetTagByIdAsync(int id)
    {
        return _tagRepository.GetTagByIdAsync(id);
    }

    public async Task<Tag?> UpdateTagAsync(Tag updateTag)
    {
        var toUpdateTag = await _tagRepository.FindAsync(updateTag.Id); 

        if (toUpdateTag == null || toUpdateTag.UserId != updateTag.UserId)
        {
            return null;
        }

        toUpdateTag.Name = updateTag.Name;
        toUpdateTag.Color = updateTag.Color;
        toUpdateTag.UserId = updateTag.UserId;

        await _tagRepository.UpdateAsync(toUpdateTag);

        return toUpdateTag;
    }
}
