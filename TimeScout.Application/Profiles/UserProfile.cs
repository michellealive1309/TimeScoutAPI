using AutoMapper;
using TimeScout.Application.DTOs.Authentication;
using TimeScout.Application.DTOs.EventGroup;
using TimeScout.Application.DTOs.User;
using TimeScout.Domain.Entities;

namespace TimeScout.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterRequestDto, User>();
        CreateMap<UserUpdateRequestDto, User>();
        CreateMap<User, UserResponseDto>();
        CreateMap<MemberRequestDto, User>();
    }
}
