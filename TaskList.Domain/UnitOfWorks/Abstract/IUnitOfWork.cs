namespace TaskList.Domain.UnitOfWorks.Abstract;

public interface IUnitOfWork : IDisposable
{
    IRepository<TaskList.Domain.Model.Task> TaskRepository { get; set; }
    bool Commit();
}