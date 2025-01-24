using TimeScout.API.DTOs.User;

namespace TimeScout.API.DTOs.EventGroup;

public class EventGroupResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<UserResponseDto>? Members { get; set; }
}
