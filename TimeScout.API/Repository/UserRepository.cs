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

    public Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        return _context.Users.AsNoTracking()
            .Where(u => u.Email == email && u.Password == password)
            .FirstOrDefaultAsync();
    }

    public Task<User> GetUserByRefreshTokenAsync(string refreshToken)
    {
        return _context.Users.AsNoTracking()
            .Where(u => u.RefreshToken == refreshToken)
            .FirstOrDefaultAsync();
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
}
