namespace TaskList.Domain.UnitOfWorks.Abstract;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    Task Add(TEntity obj);
    Task<TEntity> GetById<TKey>(TKey id);
    Task<IQueryable<TEntity>> GetAll();
    Task Update(TEntity obj);
    Task Remove<TKey>(TKey id);
}