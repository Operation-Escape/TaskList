using Microsoft.EntityFrameworkCore;
using TaskList.Domain.UnitOfWorks.Abstract;
using TaskModel = TaskList.Domain.Model.Task;

namespace TaskList.Domain.UnitOfWorks.UnitOfWorkForSql
{
    public class TaskRepository : IRepository<TaskModel>
    {
        private readonly SqlContext _db;

        public TaskRepository(SqlContext db)
        {
            _db = db;
        }

        public async Task Add(TaskModel model)
        {
            await _db.Tasks.AddAsync(model);
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<TaskModel>> GetAll() => await _db.Tasks.ToListAsync();

        public async Task<TaskModel?> GetById<TKey>(TKey id) => await _db.Tasks.FindAsync(id);

        public async Task Remove<TKey>(TKey id)
        {
            var model = await _db.Tasks.FindAsync(id);
            if (model != null)
                _db.Tasks.Remove(model);
        }

        public Task Update(TaskModel obj)
        {
            _db.Entry(obj).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
