using MongoDB.Driver;
using TaskList.Domain.Contexts.Abstract;
using TaskList.Domain.Models;
using TaskList.Domain.Models.Abstract;
using TaskList.Domain.Repositories.Abstract;
using Task = System.Threading.Tasks.Task;

namespace TaskList.Domain.Repositories
{
    public class MongoRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : SimpleDomainModel<TKey>
    {
        protected readonly IMongoContext Context;
        protected readonly IMongoCollection<TEntity> DbSet;

        protected MongoRepository(IMongoContext context)
        {
            Context = context;
            DbSet = Context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual async Task Add(TEntity obj)
        {
            obj.Id = await NextValue();
            Context.AddCommand(() => DbSet.InsertOneAsync(obj));
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq(x => x.Id, id));
            return data.SingleOrDefault();
        }

        public virtual void Remove(TKey id)
        {
            Context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq(x => x.Id, id)));
        }

        public virtual void Update(TEntity obj)
        {
            Context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(x => x.Id, obj.Id), obj));
        }

        public void Dispose()
        {
            Context?.Dispose();
            GC.SuppressFinalize(this);
        }

        private async Task<TKey> NextValue(int reservationCount = 1)
        {
            var filter = Builders<Sequnce<TKey>>.Filter.Eq(x => x.Name, typeof(TEntity).Name);

            var update = typeof(TKey) == typeof(long)
                ? Builders<Sequnce<TKey>>.Update.Inc(nameof(Sequnce<TKey>.Value), (long)reservationCount)
                : Builders<Sequnce<TKey>>.Update.Inc(nameof(Sequnce<TKey>.Value), reservationCount);

            var opt = new FindOneAndUpdateOptions<Sequnce<TKey>> { IsUpsert = true, ReturnDocument = ReturnDocument.After };

            var counterCol = Context.GetCollection<Sequnce<TKey>>("sequnces");

            var result = await counterCol.FindOneAndUpdateAsync(
                filter, update, options: opt);

            return result.Value;
        }
    }
}
