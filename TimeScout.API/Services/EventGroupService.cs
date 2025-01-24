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

    public Task<IEnumerable<EventGroup>> GetAllEventGroupAsync(int userId)
    {
        return _eventGroupRepository.GetAllEventGroupAsync(userId);
    }

    public Task<EventGroup?> GetEventGroupByIdAsync(int id, int userId)
    {
        return _eventGroupRepository.GetEventGroupByIdAsync(id, userId);
    }
}
