using TimeScout.API.Entity;

namespace TimeScout.API.Models;

public class Tag : BaseEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Color { get; set; }
    public int UserId { get; set; }
}
