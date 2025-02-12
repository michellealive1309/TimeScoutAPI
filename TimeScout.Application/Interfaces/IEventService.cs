using TimeScout.Domain.Entities;

namespace TimeScout.Application.Interfaces;

public interface IEventService
{
    Task<bool> CreateEventAsync(Event newEvent);
    Task<Event?> GetEventByIdAsync(int id);
    Task<IEnumerable<Event>> GetAllEventsAsync(string span, DateTime date);
    Task<Event?> UpdateEventAsync(Event updateEvent);
    Task<bool> DeleteEventAsync(int id, int userId);
}
