using System;
using TimeScout.API.Models;

namespace TimeScout.API.Repository;

public interface IEventGroupRepository : IRepository<EventGroup>
{
    Task<EventGroup?> FindEventGroupWithMemberByIdAsync(int id);
    Task<IEnumerable<EventGroup>> GetAllEventGroupAsync(int userId);
    Task<EventGroup?> GetEventGroupByIdAsync(int id, int userId);
    Task<IEnumerable<User>> GetMembersAsync(IEnumerable<User> members);
}
