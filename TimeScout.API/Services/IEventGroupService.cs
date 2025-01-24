using System;
using TimeScout.API.Models;

namespace TimeScout.API.Services;

public interface IEventGroupService
{
    Task<bool> CreateEventGroupAsync(EventGroup eventGroup);
    Task<IEnumerable<EventGroup>> GetAllEventGroupAsync(int userId);
    Task<EventGroup?> GetEventGroupByIdAsync(int id, int userId);
}
