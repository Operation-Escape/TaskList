using Microsoft.EntityFrameworkCore;
using TaskList.Domain.Contexts;
using TaskList.Domain.Models.Abstract;
using TaskList.Domain.Repositories.Abstract;

namespace TaskList.Domain.Repositories
{
    public class SqlRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : SimpleDomainModel<TKey>
    {
        private readonly SqlContext _context;
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

        public async Task AddAsync(TEntity model)
        {
            await DbSet.AddAsync(model);
        }

        public async Task<TEntity?> GetByIdAsync(TKey id) => await DbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public Task UpdateAsync(TEntity model)
        {
            DbSet.Update(model);
            return Task.CompletedTask;
        }

        public async Task RemoveAsync(TKey id)
        {
            var model = await DbSet.FindAsync(id);
            if (model != null)
                DbSet.Remove(model);
        }
    }
}
