using Microsoft.EntityFrameworkCore;
using TaskList.Domain.Contexts;
using TaskList.Domain.Models.Abstract;
using TaskList.Domain.Repositories.Abstract;

namespace TaskList.Domain.Repositories
{
    public class SqlRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : SimpleDomainModel<TKey>
    {
        protected readonly SqlContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public SqlRepository(SqlContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Add(TEntity model)
        {
            DbSet.Add(model);
        }

        public async Task<TEntity?> GetByIdAsync(TKey id) => await DbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual void Update(TEntity model)
        {
            DbSet.Update(model);
        }

        public void Remove(TKey id)
        {
            DbSet.Remove(DbSet.Find(id));
        }
    }
}
