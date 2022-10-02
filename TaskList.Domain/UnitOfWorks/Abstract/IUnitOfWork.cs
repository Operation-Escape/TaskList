namespace TaskList.Domain.UnitOfWorks.Abstract;

public interface IUnitOfWork : IDisposable
{
    IRepository<TaskList.Domain.Model.Task> Task { get; set; }
    bool Commit();
}