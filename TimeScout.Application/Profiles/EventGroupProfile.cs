using AutoMapper;
using TimeScout.Application.DTOs.EventGroup;
using TimeScout.Domain.Entities;

namespace TimeScout.Application.Profiles;

public class EventGroupProfile : Profile
{
    public EventGroupProfile()
    {
        CreateMap<EventGroup, EventGroupResponseDto>();

        CreateMap<EventGroupCreateRequestDto, EventGroup>();

        CreateMap<EventGroupUpdateRequestDto, EventGroup>();
    }
}
