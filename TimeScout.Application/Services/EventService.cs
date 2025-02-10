using TimeScout.Domain.Entities;
using TimeScout.Domain.Interfaces;
using TimeScout.Application.Interfaces;

namespace TimeScout.Application.Services;

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

    public async Task<IEnumerable<Event>> GetAllEventsAsync(string span, DateTime date, int userId)
    {
        var start = new DateOnly(date.Year, date.Month, date.Day);
        var end = new DateOnly(date.Year, date.Month, date.Day);
        switch (span)
        {
            case "day":
                break;
            case "week":
                start = GetStartOfWeekDate(start);
                end = GetEndOfWeekDate(end);
                break;
            case "biweek":
                start = GetStartOfWeekDate(start.AddDays(-7));
                end = GetEndOfWeekDate(end);
                break;
            case "month":
                start = new DateOnly(start.Year, start.Month, 1);
                end = start.AddMonths(1).AddDays(-1);
                break;
            case "year":
                start = new DateOnly(start.Year, 1, 1);
                end = new DateOnly(end.Year, 12, 31);
                break;
            default:
                break;
        }

        return await _eventRepository.GetEventsByDateRangeAsync(start, end, userId);
    }

    public Task<Event?> GetEventByIdAsync(int id, int userId)
    {
        return _eventRepository.GetEventByIdAsync(id, userId);
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
        toUpdateEvent.TagId = updateEvent.TagId;
        toUpdateEvent.UserId = updateEvent.UserId;

        await _eventRepository.UpdateAsync(toUpdateEvent);

        return toUpdateEvent;
    }

    private DateOnly GetStartOfWeekDate(DateOnly date)
    {
        var diff = (7 + (date.DayOfWeek - DayOfWeek.Sunday)) % 7;

        return date.AddDays(-1 * diff);
    }

    private DateOnly GetEndOfWeekDate(DateOnly date)
    {
        var diff = (7 + (date.DayOfWeek - DayOfWeek.Saturday)) % 7;

        return date.AddDays(diff);
    }
}
