using AutoMapper;
using TimeScout.API.DTOs.EventGroup;
using TimeScout.API.Models;

namespace TimeScout.API.Profiles;

public class EventGroupProfile : Profile
{
    public EventGroupProfile()
    {
        CreateMap<EventGroup, EventGroupResponseDto>();

        CreateMap<EventGroupCreateRequestDto, EventGroup>();

        CreateMap<EventGroupUpdateRequestDto, EventGroup>();
    }
}
