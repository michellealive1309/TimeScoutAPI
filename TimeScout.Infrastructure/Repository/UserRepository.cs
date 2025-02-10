using Microsoft.EntityFrameworkCore;
using TimeScout.Infrastructure.DataAccess;
using TimeScout.Domain.Entities;
using TimeScout.Domain.Interfaces;

namespace TimeScout.Infrastructure.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly TimeScoutDbContext _context;

    public UserRepository(TimeScoutDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> CheckIfUserExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
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

    public Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        return _context.Users.AsNoTracking()
            .Where(u => u.Email == email && u.Password == password)
            .FirstOrDefaultAsync();
    }

    public Task<User?> GetUserByIdAsync(int userId)
    {
        return _context.Users.AsNoTracking().IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
    {
        return _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }

    public async Task<bool> RecoverUserAsync(int userId)
    {
        var user = await _context.Users.IgnoreQueryFilters().FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        user.RecoveryEndDate = null;
        user.IsDeleted = false;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateRefreshTokenAsync(int userId, string newRefreshToken)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            return false;
        }

        user.RefreshToken = newRefreshToken;
        await _context.SaveChangesAsync();

        return true;
    }
}
