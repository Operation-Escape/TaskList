using MongoDB.Driver;

namespace TaskList.Domain.UnitOfWorks.MongoRepository.Abstract
{
    public interface IMongoContext : IDisposable
    {
        void AddCommand(Func<Task> func);
        Task<int> SaveChangesAsync();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
