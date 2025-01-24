using System;

namespace TimeScout.API.DTOs.User;

public class UserResponseDto
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
