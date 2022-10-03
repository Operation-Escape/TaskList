using MongoDB.Bson.Serialization.Attributes;

namespace TaskList.Domain.Models.Abstract
{
    public abstract class SimpleDomainModel<T>
    {
        [BsonId]
        public T Id { get; set; }
    }
}
