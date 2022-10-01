using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskList.Application.Abstract;
using TaskList.Domain;
using TaskList.Dto.Enums;
using TaskList.Dto.Task;
using TaskModel = TaskList.Domain.Model.Task;

namespace TaskList.Application.CommandHandlers;

public class TaskCommandHandler : ITaskCommandHandler
{
    private readonly SqlContext _sqlContext;
    private readonly IMapper _mapper;

    public TaskCommandHandler(SqlContext sqlContext, IMapper mapper)
    {
        _sqlContext = sqlContext;
        _mapper = mapper;
    }

    public async Task UpdateFullAsync(TaskCreateUpdateCommand cmd)
    {
        var task = await _sqlContext.Tasks.FirstOrDefaultAsync(x => x.Id == cmd.Id);
        if(task == null)
            return;
        task.Name = cmd.Name;
        task.State = cmd.State;
        task.Description = cmd.Description;
        task.RemainingWork = cmd.RemainingWork;
        task.CompletedWork = cmd.CompletedWork;
        task.OrginalEstimate = cmd.OrginalEstimate;
        _sqlContext.Update(task);
        await _sqlContext.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(TaskCreateUpdateCommand cmd)
    {
        var task = _mapper.Map<TaskModel>(cmd);
        _sqlContext.Update(task);
        await _sqlContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var task = await _sqlContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        if(task == null)
            return;
        _sqlContext.Tasks.Remove(task);
        await _sqlContext.SaveChangesAsync();
    }

    public async Task InsertAsync(TaskCreateUpdateCommand cmd)
    {
        var newTask = _mapper.Map<TaskModel>(cmd);
        await _sqlContext.Tasks.AddAsync(newTask);
        await _sqlContext.SaveChangesAsync();
    }
    
    public async Task ResolveTaskAsync(Guid id, TaskResolveCommand cmd)
    {
        var updateCommand = new TaskCreateUpdateCommand
        {
            Id = id,
            State = (int)ETaskState.Resolved,
            CompletedWork = cmd.CompletedWork,
            RemainingWork = 0
        };
        await UpdateAsync(updateCommand);
        //TODO: pubilsh to mesasage broker about resolved task, for exmaple send email message
    }
}