using System;
using AutoMapper;
using TimeScout.API.DTOs.Authentication;
using TimeScout.API.Models;

namespace TimeScout.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterRequestDto, User>();
    }
}
