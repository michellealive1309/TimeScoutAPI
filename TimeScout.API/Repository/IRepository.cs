using TimeScout.API.Entity;

namespace TimeScout.API.Repository;

public interface IRepository<TEntity>
where TEntity : BaseEntity
{
    Task AddAsync(TEntity entity);
    Task<TEntity?> FindAsync(int id);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}
