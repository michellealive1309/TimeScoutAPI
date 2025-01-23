using TimeScout.API.Models;

namespace TimeScout.API.Repository;

public interface IEventRepository : IRepository<Event>
{
    Task<Event?> GetEventByIdAsync(int id, int userId);
    Task<IEnumerable<Event>> GetEventsByDateRangeAsync(DateOnly start, DateOnly end, int userId);
}
