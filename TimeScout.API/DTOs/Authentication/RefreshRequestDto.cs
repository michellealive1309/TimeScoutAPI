using System;

namespace TimeScout.API.DTOs.Authentication;

public class RefreshRequestDto
{
    public string RefreshToken { get; set; } = null!;
}
