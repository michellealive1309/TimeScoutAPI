using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TimeScout.API.DTOs.Login;
using TimeScout.API.DTOs.Register;
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
        return _userRepository.GetUserByEmailAndPasswordAsync(email, password);
    }

    public string GenerateJSONWebToken(string email, string userId, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new ArgumentNullException("No JWT key found in configuration."));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("id", userId),
                new Claim("email", email),
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
}
