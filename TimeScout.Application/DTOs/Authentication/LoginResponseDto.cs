namespace TimeScout.Application.DTOs.Login;

public class LoginResponseDto
{
    public string AccessToken { get; set; } = null!;
    public int UserId { get; set; }
}
