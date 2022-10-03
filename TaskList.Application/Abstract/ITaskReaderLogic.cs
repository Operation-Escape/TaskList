using TaskList.Dto.Task;
using TaskList.Dto.Task.Commands;

namespace TaskList.Application.Abstract;

public interface ITaskReaderLogic
{
    public Task<List<TaskDto>> GetAllAsync(TaskSearchFilter filter);
    public Task<TaskDto> GetByIdAsync(int id);
}