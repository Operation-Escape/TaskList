using TaskList.Domain.Contexts;
using TaskList.Domain.Repositories.Abstract;
using TaskList.Domain.Repositories.TaskRepositories;
using TaskList.Domain.UnitOfWorks.Abstract;
using Task = TaskList.Domain.Models.Task;

namespace TaskList.Domain.UnitOfWorks
{
    public class SqlUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SqlContext _context;
        
        public ITaskRepository Tasks { get; set; }
        
        public SqlUnitOfWork(SqlContext context, SqlTaskRepository task)
        {
            _context = context;
            Tasks = task;
        }
        
        public Task<bool> SaveChangesAsync()
        {
            var res = _context.SaveChanges();
            return System.Threading.Tasks.Task.FromResult(res > 0);
        }
        
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
