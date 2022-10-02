using MongoDB.Bson;
using TaskList.Domain.Models.Abstract;

namespace TaskList.Domain.Models.Mongo
{
    public class Task : MongoDomainModel<ObjectId>
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// State, ETaskState
        /// </summary>
        public int State { get; private set; }
        /// <summary>
        /// Created date time
        /// </summary>
        public DateTime DateTimeCreated { get; private set; } = DateTime.Now;
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// Remaining time for work
        /// </summary>
        public decimal? OrginalEstimate { get; private set; }
        /// <summary>
        /// Remaining time for work
        /// </summary>
        public decimal? RemainingWork { get; private set; }
        /// <summary>
        /// Completed time for work
        /// </summary>
        public decimal? CompletedWork { get; private set; }
    }
}
