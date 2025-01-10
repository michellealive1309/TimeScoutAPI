using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TimeScout.API.DataAccess;
using TimeScout.API.Models;

namespace TimeScout.API.Repository;

public class UserRepository : IUserRepository
{
    private readonly TimeScoutDbContext _context;

    public UserRepository(TimeScoutDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckIfUserExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User> CreateUserAsync(User newUser)
    {

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        
        return newUser;
    }

    public async Task<bool> DeleteRefreshTokenAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            return false;
        }

        user.RefreshToken = null;
        await _context.SaveChangesAsync();

        return true;
    }

    public Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        return _context.Users.AsNoTracking()
            .Where(u => u.Email == email && u.Password == password)
            .FirstOrDefaultAsync();
    }

    public Task<User> GetUserByIdAsync(int userId)
    {
        return _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public Task<User> GetUserByRefreshTokenAsync(string refreshToken)
    {
        return _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }

    public async Task<int> UpdateRefreshTokenAsync(int userId, string newRefreshToken)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            return 0;
        }

        user.RefreshToken = newRefreshToken;
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        var userToUpdate = await _context.Users.FindAsync(user.Id);

        if (userToUpdate == null)
        {
            return null;
        }

        userToUpdate.Username = user.Username;
        userToUpdate.FirstName = user.FirstName;
        userToUpdate.LastName = user.LastName;

        await _context.SaveChangesAsync();

        return userToUpdate;
    }
}
