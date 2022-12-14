using AutoMapper;
using TaskList.Application.Abstract;
using TaskList.Domain.UnitOfWorks;
using TaskList.Domain.UnitOfWorks.Abstract;
using TaskList.Dto.Task;
using TaskList.Dto.Task.Commands;
using TaskModel = TaskList.Domain.Models.Task;

namespace TaskList.Application.ReaderLogics;

public class TaskReaderLogic : ITaskReaderLogic
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public TaskReaderLogic(SqlUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<TaskDto>> GetAllAsync(TaskSearchFilter filter)
    {
        var tasks = await _unitOfWork.Tasks.GetAllAsync(filter.Skip, filter.Limit, filter.OrderType);
        return _mapper.Map<List<TaskDto>>(tasks);
    }

    public async Task<TaskDto> GetByIdAsync(int id)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(id);
        return _mapper.Map<TaskDto>(task);
    }
}