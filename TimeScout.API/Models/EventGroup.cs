using System;

namespace TimeScout.API.Models;

public class EventGroup
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Event>? Events { get; set; }
    public ICollection<User>? Members { get; set; }
}
