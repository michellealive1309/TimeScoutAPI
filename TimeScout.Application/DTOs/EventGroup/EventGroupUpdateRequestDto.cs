using System;

namespace TimeScout.Application.DTOs.EventGroup;

public class EventGroupUpdateRequestDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<MemberRequestDto>? Members { get; set; }
}
