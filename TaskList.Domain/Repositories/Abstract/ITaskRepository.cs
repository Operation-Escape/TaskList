namespace TaskList.Domain.Repositories.Abstract;

public interface ITaskRepository : IRepository<TaskList.Domain.Models.Task, int>
{
    Task<IEnumerable<TaskList.Domain.Models.Task>> GetAllAsync(int? skip, int? limit, int orderDirection);
}