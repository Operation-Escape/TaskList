using TaskList.Dto.Task;

namespace TaskList.Application.Abstract;

public interface ITaskCommandHandler
{
    //public Task UpdateFullAsync(TaskCreateUpdateCommand cmd);
    public Task UpdateAsync(TaskCreateUpdateCommand cmd);
    public Task InsertAsync(TaskCreateUpdateCommand cmd);
    public Task DeleteAsync(int id);
    public Task ResolveTaskAsync(int id, TaskResolveCommand cmd);
}