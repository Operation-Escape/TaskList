using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using TaskList.Domain.Models.Abstract;
using TaskList.Domain.UnitOfWorks.Abstract;
using TaskList.Domain.UnitOfWorks.MongoRepository.Abstract;

namespace TaskList.Domain.UnitOfWorks.MongoRepository
{
    public class MongoRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : IModel<TKey>
    {
        protected readonly IMongoContext Context;
        protected readonly IMongoCollection<TEntity> DbSet;

        protected MongoRepository(IMongoContext context)
        {
            Context = context;
            DbSet = Context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public Task AddAsync(TEntity obj)
        {
            Context.AddCommand(() => DbSet.InsertOneAsync(obj));
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
            Context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq(x => x.Id, id)));
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TEntity obj)
        {
            Context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(x => x.Id, obj.Id), obj));
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
