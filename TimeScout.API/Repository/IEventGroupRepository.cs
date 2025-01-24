using System;
using TimeScout.API.Models;

namespace TimeScout.API.Repository;

public interface IEventGroupRepository : IRepository<EventGroup>
{
    Task<EventGroup?> GetEventGroupById(int id, int userId);
    Task<IEnumerable<EventGroup>> GetAllEventGroup(int userId);
}
