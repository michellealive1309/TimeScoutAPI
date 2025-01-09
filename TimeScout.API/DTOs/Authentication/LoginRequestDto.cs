using System;

namespace TimeScout.API.DTOs.Login;

public class LoginRequestDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
