using System;
using TimeScout.API.Models;

namespace TimeScout.API.Services;

public interface IEventService
{
    Task<bool> CreateEventAsync(Event newEvent);
    Task<Event?> GetEventByIdAsync(int id, int userId);
    Task<Event?> UpdateEventAsync(Event updateEvent);
    Task<bool> DeleteEventAsync(int id, int userId);
}
