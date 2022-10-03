using MongoDB.Driver.Core.Servers;
using TaskList.Domain.Models.Abstract;

namespace TaskList.Domain.Models
{
    public class Task : SimpleDomainModel<int>
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

        public void SetState(int state)
        {
            State = state;
        }
        
        public void SetCompletedWork(decimal? completedWork)
        {
            CompletedWork = completedWork;
        }
        
        public void SetRemainingWork(decimal? remainingWork)
        {
            RemainingWork = remainingWork;
        }
    }
}
