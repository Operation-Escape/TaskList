using TaskList.Domain.Contexts.Abstract;
using TaskList.Domain.UnitOfWorks.Abstract;
using TaskList.Domain.Repositories.Abstract;
using TaskList.Domain.Repositories.TaskRepositories;

namespace TaskList.Domain.UnitOfWorks
{
    public class MongoUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IMongoContext _context;
        
        public ITaskRepository Tasks { get; set; }

        public MongoUnitOfWork(IMongoContext context, MongoTaskRepository task)
        {
            _context = context;
            Tasks = task;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var changeAmount = await _context.SaveChangesAsync();
            return changeAmount > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
