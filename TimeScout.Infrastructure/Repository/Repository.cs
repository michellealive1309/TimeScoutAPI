using Microsoft.EntityFrameworkCore;
using TimeScout.Domain.Entities;
using TimeScout.Domain.Interfaces;

namespace TimeScout.Infrastructure.Repository;

public class Repository<TEntity> : IRepository<TEntity>
where TEntity : BaseEntity
{
    private readonly DbContext _context;

    public Repository(DbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity?> FindAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task RemoveAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }
}
