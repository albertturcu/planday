namespace Planday.Schedule.Infrastructure;

public interface IGenericRepository<TEntity>
{
   Task<IReadOnlyCollection<TEntity>> GetAllAsync();
   Task<TEntity?> GetAsync(int id);
   Task<int> AddAsync(TEntity entity);
   Task UpdateAsync(TEntity entity);
}