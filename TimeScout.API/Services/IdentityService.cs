using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TimeScout.API.Models;
using TimeScout.API.Repository;

namespace TimeScout.API.Services;

public class IdentityService : IIdentityService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public IdentityService(
        IUserRepository userRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public Task<User> AuthenticateAsync(string email, string password)
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
        await _userRepository.CreateUserAsync(newUser);

        return true;
    }

    public string GenerateJSONWebToken(string email, string userId, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new ArgumentNullException("No JWT key found in configuration."));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            ]),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        var refreshToken = Convert.ToBase64String(randomNumber);

        return refreshToken;
    }

    public Task<User> GetUserByRefreshTokenAsync(string refreshToken)
    {
        return _userRepository.GetUserByRefreshTokenAsync(refreshToken);
    }

    public Task<int> UpdateRefreshTokenAsync(int userId, string refreshToken)
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
