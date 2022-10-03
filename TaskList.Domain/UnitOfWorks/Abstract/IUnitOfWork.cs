namespace TaskList.Domain.UnitOfWorks.Abstract;
using TaskList.Domain.Repositories.Abstract;

public interface IUnitOfWork : IDisposable
{
    ITaskRepository Tasks { get; set; }
    Task<bool> SaveChangesAsync();
}