using MongoDB.Driver;
using TaskList.Domain.Contexts.Abstract;
using TaskList.Domain.Repositories.Abstract;
using TaskList.Dto.Enums;
using TaskModel = TaskList.Domain.Models.Task;

namespace TaskList.Domain.Repositories.TaskRepositories;

public class MongoTaskRepository : MongoRepository<TaskModel, int>, ITaskRepository
{
    public MongoTaskRepository(IMongoContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TaskModel>> GetAllAsync(int? skip, int? limit, EOrderDirection orderDirection)
    {
        var query = DbSet.Find(Builders<TaskModel>.Filter.Empty);

        if (orderDirection == EOrderDirection.Ascending)
            query = query.Sort(Builders<TaskModel>.Sort.Ascending(x => x.Id));
        else if (orderDirection == EOrderDirection.Descending)
            query = query.Sort(Builders<TaskModel>.Sort.Descending(x => x.Id));

        if (skip.HasValue)
            query = query.Skip(skip.Value);
        if (limit.HasValue)
            query = query.Limit(limit.Value);

        return await query.ToListAsync();
    }
}