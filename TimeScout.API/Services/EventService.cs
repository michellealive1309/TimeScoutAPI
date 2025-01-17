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

    public async Task<bool> DeleteEventAsync(int id, int userId)
    {
        var toDeleteEvent = await _eventRepository.FindAsync(id);

        if (toDeleteEvent == null || toDeleteEvent.UserId != userId)
        {
            return false;
        }

        await _eventRepository.RemoveAsync(toDeleteEvent);

        return true;
    }

    public async Task<Event?> UpdateEventAsync(Event updateEvent)
    {
        var toUpdateEvent = await _eventRepository.GetEventByIdAsync(updateEvent.Id, updateEvent.UserId); 

        if (toUpdateEvent == null || toUpdateEvent.UserId == 0)
        {
            return null;
        }

        toUpdateEvent.Name = updateEvent.Name;
        toUpdateEvent.Detail = updateEvent.Detail;
        toUpdateEvent.StartDate = updateEvent.StartDate;
        toUpdateEvent.StartTime = updateEvent.StartTime;
        toUpdateEvent.EndDate = updateEvent.EndDate;
        toUpdateEvent.EndTime = updateEvent.EndTime;
        toUpdateEvent.IsShared = updateEvent.IsShared;
        toUpdateEvent.EventGroupId = updateEvent.EventGroupId;
        toUpdateEvent.UserId = updateEvent.UserId;

        await _eventRepository.UpdateAsync(toUpdateEvent);

        return toUpdateEvent;
    }
}
