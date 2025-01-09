using System;
using TimeScout.API.Models;

namespace TimeScout.API.Repository;

public interface IUserRepository
{
    Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
    Task<User> GetUserByRefreshTokenAsync(string refreshToken);
    Task<int> UpdateRefreshTokenAsync(int userId, string newRefreshToken);
}
