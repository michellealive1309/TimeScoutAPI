using System;

namespace TimeScout.API.DTOs.EventGroup;

public class EventGroupCreateRequestDto
{
    public string? Name { get; set; }
    public IEnumerable<MemberRequestDto>? Members { get; set; }
}
