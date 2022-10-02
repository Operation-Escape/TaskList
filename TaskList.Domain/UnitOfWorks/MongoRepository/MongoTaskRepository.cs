using MongoDB.Bson;
using TaskList.Domain.UnitOfWorks.MongoRepository.Abstract;
using TaskModel = TaskList.Domain.Models.Mongo.Task;

namespace TaskList.Domain.UnitOfWorks.MongoRepository
{
    public class MongoTaskRepository : MongoRepository<TaskModel, ObjectId>, ITaskRepository
    {
        protected MongoTaskRepository(IMongoContext context) : base(context)
        {
        }
    }
}
