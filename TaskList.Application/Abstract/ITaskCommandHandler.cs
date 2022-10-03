using TaskList.Dto.Task;
using TaskList.Dto.Task.Commands;

namespace TaskList.Application.Abstract;

public interface ITaskCommandHandler
{
    public Task UpdateAsync(TaskCreateUpdateCommand cmd);
    public Task InsertAsync(TaskCreateUpdateCommand cmd);
    public Task DeleteAsync(int id);
    public Task ResolveTaskAsync(int id, TaskResolveCommand cmd);
}