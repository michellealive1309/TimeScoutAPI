using System;

namespace TimeScout.API.DTOs.Event;

public class EventCreateRequestDto
{
    public required string Name { get; set; }
    public string? Detail { get; set; }
    public string? StartDate { get; set; }
    public string? StartTime { get; set; }
    public string? EndDate { get; set; }
    public string? EndTime { get; set; }
    public bool IsShared { get; set; } = false;
    public int? EventGroupId { get; set; } = null;
    public int UserId { get; set; }
}
