using System.ComponentModel.DataAnnotations;
using TaskList.Dto.Enums;

namespace TaskList.Dto.Task.Queries;

public class TaskCreateUpdateRequest
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Name
    /// </summary>
    [StringLength(255, MinimumLength = 1)]
    public string Name { get; set; }
    /// <summary>
    /// State, ETaskState
    /// </summary>
    [EnumDataType(typeof(ETaskState))]
    public int State { get; set; }
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