using System;
using TimeScout.API.Models;

namespace TimeScout.API.Services;

public interface IIdentityService
{
    Task<User> AuthenticateAsync(string email, string password);
    Task<bool> CreateUserAsync(User newUser);
    string GenerateJSONWebToken(string email, string userId, string role);
    string GenerateRefreshToken();
    Task<User> GetUserByRefreshTokenAsync(string refreshToken);
    Task<int> UpdateRefreshTokenAsync(int userId, string newRefreshToken);
}
