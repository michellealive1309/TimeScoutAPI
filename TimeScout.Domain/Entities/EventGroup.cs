namespace TimeScout.Domain.Entities;

public class EventGroup : BaseEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Event>? Events { get; set; }
    public ICollection<User>? Members { get; set; }
}
