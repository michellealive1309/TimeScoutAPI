using TimeScout.Domain.Entities;

namespace TimeScout.Domain.Interfaces;

public interface IEventGroupRepository : IRepository<EventGroup>
{
    Task<EventGroup?> FindEventGroupWithMemberByIdAsync(int id);
    Task<IEnumerable<EventGroup>> GetAllEventGroupAsync(int userId);
    Task<EventGroup?> GetEventGroupByIdAsync(int id, int userId);
    Task<IEnumerable<User>> GetMembersAsync(IEnumerable<User> members);
}
