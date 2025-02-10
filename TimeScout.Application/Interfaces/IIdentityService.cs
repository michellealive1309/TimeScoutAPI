using TimeScout.Domain.Entities;

namespace TimeScout.Application.Interfaces;

public interface IIdentityService
{
    Task<User?> AuthenticateAsync(string email, string password);
    Task<bool> CreateUserAsync(User newUser);
    Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
    Task<bool> UpdateRefreshTokenAsync(int userId, string newRefreshToken);
}
