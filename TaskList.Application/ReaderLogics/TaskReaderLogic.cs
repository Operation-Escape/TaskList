using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskList.Application.Abstract;
using TaskList.Domain;
using TaskList.Dto.Enums;
using TaskList.Dto.Task;
using TaskModel = TaskList.Domain.Model.Task;

namespace TaskList.Application.ReaderLogics;

public class TaskReaderLogic : ITaskReaderLogic
{
    private readonly SqlContext _sqlContext;
    private readonly IMapper _mapper;
    
    public TaskReaderLogic(SqlContext sqlContext, IMapper mapper)
    {
        _sqlContext = sqlContext;
        _mapper = mapper;
    }

    public async Task<List<TaskDto>> GetAllAsync(TaskSearchFilter filter)
    {
        var query = _sqlContext.Tasks.AsQueryable();
        
        if (filter.Skip.HasValue)
            query = query.Skip(filter.Skip.Value);
        if(filter.Limit.HasValue)
            query = query.Take(filter.Limit.Value);
        
        if (filter.CreatedDateTimeOrder == (int)EOrderDirection.Ascending)
            query = query.OrderBy(x => x.DateTimeCreated);
        else if (filter.CreatedDateTimeOrder == (int)EOrderDirection.Descending)
            query = query.OrderByDescending(x => x.DateTimeCreated);
        
        var tasks = await query.ToListAsync();
        return _mapper.Map<List<TaskDto>>(tasks);
    }

    public async Task<TaskDto> GetByIdAsync(Guid id)
    {
        var task = await _sqlContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        return _mapper.Map<TaskDto>(task);
    }
}