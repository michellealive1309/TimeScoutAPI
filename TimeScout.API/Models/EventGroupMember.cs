using System;

namespace TimeScout.API.Models;

public class EventGroupMember
{
    public int EventGroupId { get; set; }
    public int UserId { get; set; }
    public EventGroup? EventGroup { get; set; }
    public User? User { get; set; }
}
