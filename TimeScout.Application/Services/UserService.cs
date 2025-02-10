using TimeScout.Domain.Entities;
using TimeScout.Domain.Interfaces;
using TimeScout.Application.Interfaces;

namespace TimeScout.Application.Services;

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

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _userRepository.FindAsync(userId);

        if (user == null)
        {
            return false;
        }

        user.RecoveryEndDate = DateTime.UtcNow.AddDays(30);
        await _userRepository.RemoveAsync(user);

        return true;
    }

    public Task<User?> GetUserByIdAsync(int userId)
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

    public async Task<User?> UpdateUserAsync(User user)
    {
        var userToUpdate = await _userRepository.FindAsync(user.Id);

        if (userToUpdate == null)
        {
            return null;
        }

        userToUpdate.Username = user.Username;
        userToUpdate.FirstName = user.FirstName;
        userToUpdate.LastName = user.LastName;

        await _userRepository.UpdateAsync(userToUpdate);

        return userToUpdate;
    }
}
