using MongoDB.Bson;
using TaskList.Domain.UnitOfWorks.Abstract;
using TaskModel = TaskList.Domain.Models.Mongo.Task;

namespace TaskList.Domain.UnitOfWorks.MongoRepository.Abstract
{
    public interface ITaskRepository : IRepository<TaskModel, ObjectId>
    {
    }
}
