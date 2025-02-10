using TimeScout.Domain.Entities;

namespace TimeScout.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> CheckIfUserExistsAsync(string email);
    Task<bool> DeleteRefreshTokenAsync(int userId);
    Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
    Task<bool> RecoverUserAsync(int userId);
    Task<bool> UpdateRefreshTokenAsync(int userId, string newRefreshToken);
}
