namespace TaskList.Domain.UnitOfWorks.Abstract;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    Task Add(TEntity obj);
    Task<TEntity> GetById<TKey>(TKey id);
    Task<IEnumerable<TEntity>> GetAll();
    Task Update(TEntity obj);
    Task Remove<TKey>(TKey id);
}