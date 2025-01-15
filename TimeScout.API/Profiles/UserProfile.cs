using System;
using AutoMapper;
using TimeScout.API.DTOs.Authentication;
using TimeScout.API.DTOs.User;
using TimeScout.API.Models;

namespace TimeScout.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterRequestDto, User>();
        CreateMap<UserUpdateRequestDto, User>();
        CreateMap<User, UserUpdateResponseDto>();
    }
}
