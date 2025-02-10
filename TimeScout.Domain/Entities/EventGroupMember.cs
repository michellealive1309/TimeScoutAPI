using System;

namespace TimeScout.Domain.Entities;

public class EventGroupMember
{
    public int EventGroupId { get; set; }
    public int UserId { get; set; }
    public EventGroup? EventGroup { get; set; }
    public User? User { get; set; }
}
