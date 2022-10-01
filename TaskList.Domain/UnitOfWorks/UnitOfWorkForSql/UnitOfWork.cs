using TaskList.Domain.UnitOfWorks.Abstract;

namespace TaskList.Domain.UnitOfWorks.UnitOfWorkForSql
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SqlContext _db;
        public IRepository<Model.Task> TaskRepository { get; set; }

        public UnitOfWork(SqlContext db)
        {
            _db = db;
            TaskRepository = new TaskRepository(db);
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
