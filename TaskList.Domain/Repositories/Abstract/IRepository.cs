using TaskList.Domain.Models.Abstract;

namespace TaskList.Domain.Repositories.Abstract;

public interface IRepository<TEntity, TKey> : IDisposable where TEntity : SimpleDomainModel<TKey>
{
    void Add(TEntity obj);
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    void Update(TEntity obj);
    void Remove(TKey id);
}