using System;
using TimeScout.API.Models;

namespace TimeScout.API.Repository;

public interface IEventGroupRepository : IRepository<EventGroup>
{
    Task<EventGroup?> GetEventGroupByIdAsync(int id, int userId);
    Task<IEnumerable<EventGroup>> GetAllEventGroupAsync(int userId);
}
