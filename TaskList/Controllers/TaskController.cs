using Microsoft.AspNetCore.Mvc;
using SerilogTimings;
using TaskList.Application.Abstract;
using TaskList.Dto.Task;

namespace TaskList.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class TaskController : Controller
{
    private readonly ITaskReaderLogic _readerLogic;
    private readonly ITaskCommandHandler _commandHandler;
    public TaskController(ITaskReaderLogic readerLogic, ITaskCommandHandler commandHandler)
    {
        _readerLogic = readerLogic;
        _commandHandler = commandHandler;
    }
    
    /// <summary>
    /// Get Task
    /// </summary>
    [HttpGet]
    public async Task<ActionResult> GetAllAsync([FromQuery]TaskSearchFilter filter)
    {
        using var op = Operation.At(Serilog.Events.LogEventLevel.Debug).Begin("get all tasks");
        var result = await _readerLogic.GetAllAsync(filter);
        return Ok(result);
    }
    
    /// <summary>
    /// Get Task /Task/5
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetTaskAsync(Guid id)
    {
        using var op = Operation.At(Serilog.Events.LogEventLevel.Debug).Begin("get task with id {0}", id);
        var result = await _readerLogic.GetByIdAsync(id);
        return Ok(result);
    }
    
    /// <summary>
    /// Insert Task /Task
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> InsertTaskAsync([FromBody]TaskCreateUpdateCommand cmd)
    {
        using var op = Operation.At(Serilog.Events.LogEventLevel.Debug).Begin("create task with body {0}", cmd);
        if (cmd == null)
            return BadRequest();
        await _commandHandler.InsertAsync(cmd);
        return Ok();
    }
    
    /// <summary>
    /// Update Task /Task/5
    /// </summary>
    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateTaskAsync(Guid id, [FromBody]TaskCreateUpdateCommand cmd)
    {
        using var op = Operation.At(Serilog.Events.LogEventLevel.Debug).Begin("update task with id {0} and body {1}", id, cmd);
        if (cmd == null)
            return BadRequest();
        cmd.Id = id;
        await _commandHandler.UpdateAsync(cmd);
        return Ok();
    }
    
    /// <summary>
    /// Full Update Task /Task/5
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTaskFullAsync(Guid id, [FromBody]TaskCreateUpdateCommand cmd)
    {
        using var op = Operation.At(Serilog.Events.LogEventLevel.Debug).Begin("update task with id {0} and body {1}", id, cmd);
        if (cmd == null)
            return BadRequest();
        cmd.Id = id;
        await _commandHandler.UpdateFullAsync(cmd);
        return Ok();
    }
    
    /// <summary>
    /// resolve Task /Task/5
    /// </summary>
    [HttpPatch("{id}/resolve")]
    public async Task<ActionResult> ResolveTaskAsync(Guid id, [FromBody]TaskResolveCommand cmd)
    {
        using var op = Operation.At(Serilog.Events.LogEventLevel.Debug).Begin("resolve task with id {0} and body {1}", id, cmd);
        await _commandHandler.ResolveTaskAsync(id, cmd);
        return Ok();
    }
    
    /// <summary>
    /// Insert Task /Task/5
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTaskAsync(Guid id)
    {
        using var op = Operation.At(Serilog.Events.LogEventLevel.Debug).Begin("delete task with id {0}", id);
        await _commandHandler.DeleteAsync(id);
        return Ok();
    }
}