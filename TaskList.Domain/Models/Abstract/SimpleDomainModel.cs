using MongoDB.Bson.Serialization.Attributes;

namespace TaskList.Domain.Models.Abstract
{
    public abstract class SimpleDomainModel<T>
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonDefaultValue(0)]
        public T Id { get; set; }
    }
}
