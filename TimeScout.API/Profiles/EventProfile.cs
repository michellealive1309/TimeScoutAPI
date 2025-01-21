using AutoMapper;
using TimeScout.API.DTOs.Event;
using TimeScout.API.Models;

namespace TimeScout.API.Profiles;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<Event, EventResponseDto>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToString("HH:mm")))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToString("HH:mm")));

        CreateMap<EventCreateRequestDto, Event>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.StartDate!, "yyyy-mm-dd")))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeOnly.ParseExact(src.StartTime!, "HH:mm")))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.EndDate!, "yyyy-mm-dd")))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeOnly.ParseExact(src.EndTime!, "HH:mm")));

        CreateMap<EventUpdateRequestDto, Event>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.StartDate!, "yyyy-mm-dd")))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeOnly.ParseExact(src.StartTime!, "HH:mm")))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.EndDate!, "yyyy-mm-dd")))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeOnly.ParseExact(src.EndTime!, "HH:mm")));
    }
}
