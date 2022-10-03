﻿using MongoDB.Driver;
using TaskList.Domain.Contexts.Abstract;
using TaskList.Domain.Repositories.Abstract;
using MongoDB.Driver;
using TaskList.Dto.Enums;
using TaskModel = TaskList.Domain.Models.Task;

namespace TaskList.Domain.Repositories.TaskRepositories;

public class MongoTaskRepository : MongoRepository<TaskModel, int>, ITaskRepository
{
    protected MongoTaskRepository(IMongoContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TaskModel>> GetLimitedTasksAsync(int? skip, int? limit, int orderDirection)
    {
        var query = DbSet.Find(Builders<TaskModel>.Filter.Empty);

        if (orderDirection == (int)EOrderDirection.Ascending)
            query = query.Sort(Builders<TaskModel>.Sort.Ascending(x => x.Id));
        else if (orderDirection == (int)EOrderDirection.Descending)
            query = query.Sort(Builders<TaskModel>.Sort.Descending(x => x.Id));

        if (skip.HasValue)
            query = query.Skip(skip.Value);
        if (limit.HasValue)
            query = query.Limit(limit.Value);

        return await query.ToListAsync();
    }
}