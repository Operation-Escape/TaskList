﻿using TaskList.Shared.Common;
using TaskList.Shared.Common.Sql;

namespace TaskList.Domain.Model
{
    public class Task : TypedDomainModel<Guid>
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// State, ETaskState
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// Created date time
        /// </summary>
        public DateTime DateTimeCreated { get; set; } = DateTime.Now;
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Remaining time for work
        /// </summary>
        public decimal? OrginalEstimate { get; set; }
        /// <summary>
        /// Remaining time for work
        /// </summary>
        public decimal? RemainingWork { get; set; }
        /// <summary>
        /// Completed time for work
        /// </summary>
        public decimal? CompletedWork { get; set; }
    }
}
