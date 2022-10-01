namespace TaskList.Domain.UnitOfWorks.Abstract;

public interface IUnitOfWork : IDisposable
{
    bool Commit();
}