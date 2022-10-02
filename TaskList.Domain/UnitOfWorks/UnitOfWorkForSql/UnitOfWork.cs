using TaskList.Domain.UnitOfWorks.Abstract;
using TaskModel = TaskList.Domain.Model.Task;

namespace TaskList.Domain.UnitOfWorks.UnitOfWorkForSql
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SqlContext _db;

        public IRepository<TaskModel> Task { get; set; }

        public UnitOfWork(SqlContext db, IRepository<TaskModel> taskRepository)
        {
            _db = db;
            Task = taskRepository;
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }

        public bool Commit()
        {
            var res = _db.SaveChanges();
            return res > 0;
        }
    }
}
