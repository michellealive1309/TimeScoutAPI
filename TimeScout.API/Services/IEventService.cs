using System;
using TimeScout.API.Models;

namespace TimeScout.API.Services;

public interface IEventService
{
    Task<bool> CreateEventAsync(Event newEvent);
}
