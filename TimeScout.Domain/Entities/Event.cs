namespace TimeScout.Domain.Entities;

public class Event : BaseEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Detail { get; set; }
    public DateOnly StartDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public DateOnly EndDate { get; set; }
    public TimeOnly EndTime { get; set; }
    public TimeSpan Duration => EndTime.ToTimeSpan() - StartTime.ToTimeSpan();
    public bool IsShared { get; set; }
    public int? EventGroupId { get; set; }
    public int? TagId { get; set; }
    public int UserId { get; set; }
    public EventGroup? EventGroup { get; set; }
    public Tag? Tag { get; set; }
    public User? User { get; set; }
}
