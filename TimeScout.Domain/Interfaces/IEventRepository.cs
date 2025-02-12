using TimeScout.Domain.Entities;

namespace TimeScout.Domain.Interfaces;

public interface IEventRepository : IRepository<Event>
{
    Task<Event?> GetEventByIdAsync(int id);
    Task<IEnumerable<Event>> GetEventsByDateRangeAsync(DateOnly start, DateOnly end);
}
