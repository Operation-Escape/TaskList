using Microsoft.EntityFrameworkCore;
using TaskList.Domain.Contexts;
using TaskList.Domain.Repositories.Abstract;
using TaskList.Dto.Enums;
using TaskModel = TaskList.Domain.Models.Task;

namespace TaskList.Domain.Repositories.TaskRepositories;

public class SqlTaskRepository : SqlRepository<TaskModel, int>, ITaskRepository
{
    public SqlTaskRepository(SqlContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TaskModel>> GetAllAsync(int? skip, int? limit, int orderDirection)
    {
        var query = DbSet.AsQueryable();
        
        if (orderDirection == (int)EOrderDirection.Ascending)
            query = query.OrderBy(x => x.DateTimeCreated);
        else if (orderDirection == (int)EOrderDirection.Descending)
            query = query.OrderByDescending(x => x.DateTimeCreated);
        
        if (skip.HasValue)
            query = query.Skip(skip.Value);
        if(limit.HasValue)
            query = query.Take(limit.Value);

        var tasks = await query.ToListAsync();

        return tasks;
    }
}