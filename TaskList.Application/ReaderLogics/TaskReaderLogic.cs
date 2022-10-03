using AutoMapper;
using TaskList.Application.Abstract;
using TaskList.Domain.UnitOfWorks.Abstract;
using TaskList.Dto.Task;
using TaskModel = TaskList.Domain.Models.Task;

namespace TaskList.Application.ReaderLogics;

public class TaskReaderLogic : ITaskReaderLogic
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public TaskReaderLogic(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<TaskDto>> GetAllAsync(TaskSearchFilter filter)
    {
        var tasks = await _unitOfWork.Tasks.GetLimitedTasksAsync(filter.Limit, filter.Skip, filter.OrderType);
        
        return _mapper.Map<List<TaskDto>>(tasks);
    }

    public async Task<TaskDto> GetByIdAsync(int id)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(id);
        return _mapper.Map<TaskDto>(task);
    }
}