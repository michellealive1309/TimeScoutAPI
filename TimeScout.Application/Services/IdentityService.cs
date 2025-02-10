using System.Security.Cryptography;
using System.Text;
using TimeScout.Domain.Entities;
using TimeScout.Domain.Interfaces;
using TimeScout.Application.Interfaces;

namespace TimeScout.Application.Services;

public class IdentityService : IIdentityService
{
    private readonly IUserRepository _userRepository;

    public IdentityService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<User?> AuthenticateAsync(string email, string password)
    {
        return _userRepository.GetUserByEmailAndPasswordAsync(email, HashPassword(password));
    }

    public async Task<bool> CreateUserAsync(User newUser)
    {
        if (await _userRepository.CheckIfUserExistsAsync(newUser.Email))
        {
            return false;
        }

        newUser.Password = HashPassword(newUser.Password);
        newUser.Role = "User";
        await _userRepository.AddAsync(newUser);

        return true;
    }

    public Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
    {
        return _userRepository.GetUserByRefreshTokenAsync(refreshToken);
    }

    public Task<bool> UpdateRefreshTokenAsync(int userId, string refreshToken)
    {
        return _userRepository.UpdateRefreshTokenAsync(userId, refreshToken);
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

        return hashedPassword;
    }
}
