using System;

namespace TimeScout.API.DTOs.Event;

public class EventResponseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Detail { get; set; }
    public string? StartDate { get; set; }
    public string? StartTime { get; set; }
    public string? EndDate { get; set; }
    public string? EndTime { get; set; }
    public TimeSpan Duration { get; set; }
    public bool IsShared { get; set; }
    public int? EventGroupId { get; set; }
    public int? TagId { get; set; }
    public int UserId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
