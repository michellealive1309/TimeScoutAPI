namespace TimeScout.Domain.Entities;

public class Tag : BaseEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Color { get; set; }
    public int UserId { get; set; }
}
