using System;
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

    public Task<IEnumerable<EventGroup>> GetAllEventGroupAsync(int userId)
    {
        return _eventGroupRepository.GetAllEventGroupAsync(userId);
    }

    public Task<EventGroup?> GetEventGroupByIdAsync(int id, int userId)
    {
        return _eventGroupRepository.GetEventGroupByIdAsync(id, userId);
    }
}
