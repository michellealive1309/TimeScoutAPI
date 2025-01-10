using System;

namespace TimeScout.API.DTOs.Login;

public class LoginResponseDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public int UserId { get; set; }
}
