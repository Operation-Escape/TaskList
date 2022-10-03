using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SerilogTimings;
using TaskList.Application.Abstract;
using TaskList.Dto.Task.Commands;
using TaskList.Dto.Task.Queries;

namespace TaskList.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class TaskController : Controller
{
    private readonly ITaskReaderLogic _readerLogic;
    private readonly ITaskCommandHandler _commandHandler;
    private readonly IMapper _mapper;
    
    public TaskController(ITaskReaderLogic readerLogic, ITaskCommandHandler commandHandler, IMapper mapper)
    {
        _readerLogic = readerLogic;
        _commandHandler = commandHandler;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Get Task
    /// </summary>
    [HttpGet]
    public async Task<ActionResult> GetAllAsync([FromQuery]TaskSearchRequest request)
    {
        using var op = Operation.At(Serilog.Events.LogEventLevel.Debug).Begin("get all tasks");
        var filter = _mapper.Map<TaskSearchFilter>(request);
        var result = await _readerLogic.GetAllAsync(filter);
        return Ok(result);
    }
    
    /// <summary>
    /// Get Task /Task/5
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetTaskAsync(int id)
    {
        using var op = Operation.At(Serilog.Events.LogEventLevel.Debug).Begin("get task with id {0}", id);
        var result = await _readerLogic.GetByIdAsync(id);
        return Ok(result);
    }
    
    /// <summary>
    /// Insert Task /Task
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> InsertTaskAsync([FromBody]TaskCreateUpdateRequest request)
    {
        using var op = Operation.At(Serilog.Events.LogEventLevel.Debug).Begin("create task with body {0}", request);
        if (request == null)
            return BadRequest();
        var cmd = _mapper.Map<TaskCreateUpdateCommand>(request);
        await _commandHandler.InsertAsync(cmd);
        return Ok();
    }
    
    /// <summary>
    /// Update Task /Task/5
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTaskAsync(int id, [FromBody]TaskCreateUpdateRequest request)
    {
        using var op = Operation.At(Serilog.Events.LogEventLevel.Debug).Begin("update task with id {0} and body {1}", id, request);
        var cmd = _mapper.Map<TaskCreateUpdateCommand>(request);
        if (cmd == null)
            return BadRequest();
        cmd.Id = id;
        await _commandHandler.UpdateAsync(cmd);
        return Ok();
    }

    /// <summary>
    /// resolve Task /Task/resolve
    /// </summary>
    [HttpPatch("{id}/resolve")]
    public async Task<ActionResult> ResolveTaskAsync(int id, [FromBody]TaskResolveRequest request)
    {
        using var op = Operation.At(Serilog.Events.LogEventLevel.Debug).Begin("resolve task with id {0} and body {1}", id, request);
        var cmd = _mapper.Map<TaskResolveCommand>(request);
        await _commandHandler.ResolveTaskAsync(id, cmd);
        return Ok();
    }
    
    /// <summary>
    /// delete Task /Task/5
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTaskAsync(int id)
    {
        using var op = Operation.At(Serilog.Events.LogEventLevel.Debug).Begin("delete task with id {0}", id);
        await _commandHandler.DeleteAsync(id);
        return Ok();
    }
}