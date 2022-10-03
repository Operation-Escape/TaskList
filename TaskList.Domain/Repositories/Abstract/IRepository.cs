using TaskList.Domain.Models.Abstract;

namespace TaskList.Domain.Repositories.Abstract;

public interface IRepository<TEntity, TKey> : IDisposable where TEntity : SimpleDomainModel<TKey>
{
    Task AddAsync(TEntity obj);
    Task<TEntity> GetByIdAsync(TKey id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task UpdateAsync(TEntity obj);
    Task RemoveAsync(TKey id);
}