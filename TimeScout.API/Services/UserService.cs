using System;
using TimeScout.API.Models;
using TimeScout.API.Repository;

namespace TimeScout.API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<bool> DeleteRefreshTokenAsync(int userId)
    {
        return _userRepository.DeleteRefreshTokenAsync(userId);
    }

    public Task<User> GetUserByIdAsync(int userId)
    {
        return _userRepository.GetUserByIdAsync(userId);
    }

    public Task<User> UpdateUserAsync(User user)
    {
        return _userRepository.UpdateUserAsync(user);
    }
}
