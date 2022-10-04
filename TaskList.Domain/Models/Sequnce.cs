using MongoDB.Bson.Serialization.Attributes;

namespace TaskList.Domain.Models
{
    public class Sequnce<T>
    {
        public T Value { get; set; }

        [BsonId]
        public string Name { get; set; }
    }

}
