using AutoMapper;
using TaskList.Application.Abstract;
using TaskList.Domain.UnitOfWorks;
using TaskList.Domain.UnitOfWorks.Abstract;
using TaskList.Dto.Enums;
using TaskList.Dto.Task;
using TaskModel = TaskList.Domain.Models.Task;

namespace TaskList.Application.CommandHandlers;

public class TaskCommandHandler : ITaskCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskCommandHandler(MongoUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task UpdateAsync(TaskCreateUpdateCommand cmd)
    {
        var task = _mapper.Map<TaskModel>(cmd);
        await _unitOfWork.Tasks.UpdateAsync(task);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.Tasks.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task InsertAsync(TaskCreateUpdateCommand cmd)
    {
        var newTask = _mapper.Map<TaskModel>(cmd);
        await _unitOfWork.Tasks.AddAsync(newTask);
        await _unitOfWork.SaveChangesAsync();
    }
    
    public async Task ResolveTaskAsync(int id, TaskResolveCommand cmd)
    {
        var updateCommand = new TaskCreateUpdateCommand
        {
            Id = id,
            State = (int)ETaskState.Resolved,
            CompletedWork = cmd.CompletedWork,
            RemainingWork = 0
        };
        var task = _mapper.Map<TaskModel>(updateCommand);
        await _unitOfWork.Tasks.UpdateAsync(task);
        //TODO: pubilsh to mesasage broker about resolved task, for exmaple send email message
    }
}