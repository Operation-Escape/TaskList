using TaskList.Dto.Task;
using TaskModel = TaskList.Domain.Model.Task;

namespace TaskList.Application.Abstract;

public interface ITaskCommandHandler
{
    public Task UpdateFullAsync(TaskCreateUpdateCommand cmd);
    public Task UpdateAsync(TaskCreateUpdateCommand cmd);
    public Task InsertAsync(TaskCreateUpdateCommand cmd);
    public Task DeleteAsync(Guid id);
    public Task ResolveTaskAsync(Guid id, TaskResolveCommand cmd);
}