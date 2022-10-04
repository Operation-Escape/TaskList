using AutoMapper;
using TaskList.Application.Abstract;
using TaskList.Domain.UnitOfWorks;
using TaskList.Domain.UnitOfWorks.Abstract;
using TaskList.Dto.Enums;
using TaskList.Dto.Task.Commands;
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
        if(!cmd.IsValid())
            throw new ArgumentException("Invalid arguments");
        var task = _mapper.Map<TaskModel>(cmd);
        _unitOfWork.Tasks.Update(task);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        _unitOfWork.Tasks.Remove(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task InsertAsync(TaskCreateUpdateCommand cmd)
    {
        if(!cmd.IsValid())
            throw new ArgumentException("Invalid arguments");
        var newTask = _mapper.Map<TaskModel>(cmd);
        await _unitOfWork.Tasks.Add(newTask);
        await _unitOfWork.SaveChangesAsync();
    }
    
    public async Task ResolveTaskAsync(int id, TaskResolveCommand cmd)
    {
        if(!cmd.IsValid())
            throw new ArgumentException("Invalid argiments");
        var task = await _unitOfWork.Tasks.GetByIdAsync(id);
        if (task == null)
            return;
        task.SetState((int)ETaskState.Resolved);
        task.SetCompletedWork(cmd.CompletedWork);
        task.SetRemainingWork(0);
        _unitOfWork.Tasks.Update(task);
        await _unitOfWork.SaveChangesAsync();
        //TODO: pubilsh to mesasage broker about resolved task, for exmaple send email message
    }
}