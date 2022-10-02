using MongoDB.Bson.Serialization.Attributes;

namespace TaskList.Domain.Models.Abstract
{
    public abstract class MongoDomainModel<T> : IModel<T>
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public T Id { get; set; }
    }
}
