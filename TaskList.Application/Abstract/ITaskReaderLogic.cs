using TaskList.Dto.Task;
using TaskModel = TaskList.Domain.Model.Task;

namespace TaskList.Application.Abstract;

public interface ITaskReaderLogic
{
    public Task<List<TaskDto>> GetAllAsync(TaskSearchFilter filter);
    public Task<TaskDto> GetByIdAsync(Guid id);
}