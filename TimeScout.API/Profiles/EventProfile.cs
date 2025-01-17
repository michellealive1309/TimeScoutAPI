using AutoMapper;
using TimeScout.API.DTOs.Event;
using TimeScout.API.Models;

namespace TimeScout.API.Profiles;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<EventCreateRequestDto, Event>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.StartDate!, "yyyy-mm-dd")))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeOnly.ParseExact(src.StartTime!, "HH:mm")))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.EndDate!, "yyyy-mm-dd")))
    }
}
