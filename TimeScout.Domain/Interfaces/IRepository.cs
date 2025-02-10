using TimeScout.Domain.Entities;

namespace TimeScout.Domain.Interfaces;

public interface IRepository<TEntity>
where TEntity : BaseEntity
{
    Task AddAsync(TEntity entity);
    Task<TEntity?> FindAsync(int id);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}
