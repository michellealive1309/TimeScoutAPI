using System;
using TimeScout.API.Entity;

namespace TimeScout.API.Models;

public class User : BaseEntity
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? RefreshToken { get; set; }
    public ICollection<EventGroup>? EventGroups { get; set; }
}
