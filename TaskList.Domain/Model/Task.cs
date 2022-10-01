using TaskList.Shared.Common;
using TaskList.Shared.Common.Sql;

namespace TaskList.Domain.Model
{
    public class Task : TypedDomainModel<Guid>
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
        
        // Empty constructor for EF
        protected Task() { }
        
        public Task(string name, int state, string description, decimal orginalEstimate, decimal remainingWork, decimal completedWork)
        {
            Name = name;
            State = state;
            Description = description;
            OrginalEstimate = orginalEstimate;
            RemainingWork = remainingWork;
            CompletedWork = completedWork;
        }
    }
}
