using MongoDB.Driver;
using TaskList.Domain.Contexts.Abstract;
using TaskList.Domain.Models.Abstract;
using TaskList.Domain.Repositories.Abstract;

namespace TaskList.Domain.Repositories
{
    public class MongoRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : SimpleDomainModel<TKey>
    {
        private readonly IMongoContext _context;
        protected readonly IMongoCollection<TEntity> DbSet;

        protected MongoRepository(IMongoContext context)
        {
            _context = context;
            DbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public Task AddAsync(TEntity obj)
        {
            _context.AddCommand(() => DbSet.InsertOneAsync(obj));
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq(x => x.Id, id));
            return data.SingleOrDefault();
        }

        public Task RemoveAsync(TKey id)
        {
            _context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq(x => x.Id, id)));
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TEntity obj)
        {
            _context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(x => x.Id, obj.Id), obj));
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
