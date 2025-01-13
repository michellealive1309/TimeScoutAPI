using System;
using TimeScout.API.Models;

namespace TimeScout.API.Repository;

public interface IUserRepository
{
    Task<bool> CheckIfUserExistsAsync(string email);
    Task<User> CreateUserAsync(User newUser);
    Task<bool> DeleteRefreshTokenAsync(int userId);
    Task<bool> DeleteUserAsync(int userId);
    Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
    Task<User> GetUserByIdAsync(int userId);
    Task<User> GetUserByRefreshTokenAsync(string refreshToken);
    Task<int> UpdateRefreshTokenAsync(int userId, string newRefreshToken);
    Task<User> UpdateUserAsync(User user);
}
