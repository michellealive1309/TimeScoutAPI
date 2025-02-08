using System;
using AutoMapper;
using TimeScout.API.DTOs.Event;
using TimeScout.API.Models;

namespace TimeScout.API.Profiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<TagRequestDto, Tag>();
        CreateMap<Tag, TagResponseDto>();
    }
}
