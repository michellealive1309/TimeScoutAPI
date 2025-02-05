using TimeScout.API.Models;
using TimeScout.API.Repository;

namespace TimeScout.API.Services;

public class EventGroupService : IEventGroupService
{
    private readonly IEventGroupRepository _eventGroupRepository;

    public EventGroupService(IEventGroupRepository eventGroupRepository)
    {
        _eventGroupRepository = eventGroupRepository;
    }

    public async Task<bool> CreateEventGroupAsync(EventGroup eventGroup)
    {
        if (eventGroup.Members == null || eventGroup.Members.Count < 1)
        {
            return false;
        }

        eventGroup.Members = [.. await _eventGroupRepository.GetMembersAsync(eventGroup.Members)];

        await _eventGroupRepository.AddAsync(eventGroup);

        return true;
    }

    public async Task<bool> DeleteEventGroupAsync(int id, int userId)
    {
        var eventGroup = await _eventGroupRepository.FindEventGroupWithMemberByIdAsync(id);
        
        if (eventGroup == null)
        {
            return false;
        }

        await _eventGroupRepository.RemoveAsync(eventGroup);

        return true;
    }

    public Task<IEnumerable<EventGroup>> GetAllEventGroupAsync(int userId)
    {
        return _eventGroupRepository.GetAllEventGroupAsync(userId);
    }

    public Task<EventGroup?> GetEventGroupByIdAsync(int id, int userId)
    {
        return _eventGroupRepository.GetEventGroupByIdAsync(id, userId);
    }

    public async Task<EventGroup?> UpdateEventGroupAsync(EventGroup updateEventGroup, int userId)
    {
        var toUpdateEventGroup = await _eventGroupRepository.FindEventGroupWithMemberByIdAsync(updateEventGroup.Id);

        if (
            toUpdateEventGroup == null
            || updateEventGroup.Members == null
            || toUpdateEventGroup.Members == null
            || !toUpdateEventGroup.Members.Any(m => m.Id == userId)
        )
        {
            return null;
        }

        updateEventGroup.Members = [.. await _eventGroupRepository.GetMembersAsync(updateEventGroup.Members)];

        toUpdateEventGroup.Name = updateEventGroup.Name;
        toUpdateEventGroup.Members = updateEventGroup.Members;
        
        await _eventGroupRepository.UpdateAsync(toUpdateEventGroup);

        return toUpdateEventGroup;
    }
}
