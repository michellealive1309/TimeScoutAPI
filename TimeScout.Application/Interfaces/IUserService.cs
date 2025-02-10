using TimeScout.Domain.Entities;

namespace TimeScout.Application.Interfaces;

public interface IUserService
{
    Task<bool> DeleteRefreshTokenAsync(int userId);
    Task<bool> DeleteUserAsync(int userId);
    Task<User?> GetUserByIdAsync(int userId);
    Task<bool> RecoverUserAsync(int userId);
    Task<User?> UpdateUserAsync(User user);
}
