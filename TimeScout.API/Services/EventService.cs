using System;
using TimeScout.API.Models;
using TimeScout.API.Repository;

namespace TimeScout.API.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<bool> CreateEventAsync(Event newEvent)
    {
        if (newEvent.UserId == 0)
        {
            return false;
        }

        await _eventRepository.AddAsync(newEvent);

        return true;
    }
}
