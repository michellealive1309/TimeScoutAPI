using AutoMapper;
using TimeScout.Application.DTOs.Tag;
using TimeScout.Domain.Entities;

namespace TimeScout.Application.Profiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<TagRequestDto, Tag>();
        CreateMap<Tag, TagResponseDto>();
    }
}
