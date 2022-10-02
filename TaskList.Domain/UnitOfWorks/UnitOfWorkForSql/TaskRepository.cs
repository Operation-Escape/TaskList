using Microsoft.EntityFrameworkCore;
using TaskList.Domain.UnitOfWorks.Abstract;

namespace TaskList.Domain.UnitOfWorks.UnitOfWorkForSql
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SqlContext _db;
        private DbSet<T> _table = null;

        public Repository(SqlContext db)
        {
            _db = db;
            _table = _db.Set<T>();
        }

        public async Task Add(T model)
        {
            await _table.AddAsync(model);
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<T>> GetAll() => await _table.ToListAsync();

        public async Task<T?> GetById<TKey>(TKey id) => await _table.FindAsync(id);

        public async Task Remove<TKey>(TKey id)
        {
            var model = await _table.FindAsync(id);
            if (model != null)
                _table.Remove(model);
        }

        public Task Update(T obj)
        {
            _table.Update(obj);
            return Task.CompletedTask;
        }
    }
}
