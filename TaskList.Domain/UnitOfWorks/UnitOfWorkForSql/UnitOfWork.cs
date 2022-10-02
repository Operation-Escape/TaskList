using TaskList.Domain.UnitOfWorks.Abstract;
//using TaskModel = TaskList.Domain.Models.Task;

namespace TaskList.Domain.UnitOfWorks.UnitOfWorkForSql
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SqlContext _db;

        //public IRepository<TaskModel> TaskModel { get; set; }

        public UnitOfWork(SqlContext db/*, IRepository<TaskModel> taskRepository*/)
        {
            _db = db;
            //TaskModel = taskRepository;
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task<bool> Commit()
        {
            var res = _db.SaveChanges();
            return Task.FromResult(res > 0);
        }
    }
}
