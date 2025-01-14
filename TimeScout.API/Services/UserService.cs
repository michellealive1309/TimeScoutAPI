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

    public Task<bool> DeleteUserAsync(int userId)
    {
        return _userRepository.DeleteUserAsync(userId);
    }

    public Task<User> GetUserByIdAsync(int userId)
    {
        return _userRepository.GetUserByIdAsync(userId);
    }

    public async Task<bool> RecoverUserAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null || user.RecoveryEndDate < DateTime.UtcNow)
        {
            return false;
        }

        return await _userRepository.RecoverUserAsync(userId);
    }

    public Task<User> UpdateUserAsync(User user)
    {
        return _userRepository.UpdateUserAsync(user);
    }
}
