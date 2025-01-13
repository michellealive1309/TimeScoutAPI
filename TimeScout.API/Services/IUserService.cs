using System;
using TimeScout.API.Models;

namespace TimeScout.API.Services;

public interface IUserService
{
    Task<bool> DeleteRefreshTokenAsync(int userId);
    Task<bool> DeleteUserAsync(int userId);
    Task<User> GetUserByIdAsync(int userId);
    Task<User> UpdateUserAsync(User user);
}
