using System;

namespace TimeScout.API.DTOs.User;

public class UserUpdateRequestDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
