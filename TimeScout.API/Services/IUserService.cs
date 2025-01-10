using System;
using TimeScout.API.Models;

namespace TimeScout.API.Services;

public interface IUserService
{
    Task<User> GetUserByIdAsync(int userId);
    Task<User> UpdateUserAsync(User user);
}
