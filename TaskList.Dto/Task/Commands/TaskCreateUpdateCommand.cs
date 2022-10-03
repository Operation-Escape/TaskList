namespace TaskList.Dto.Task.Commands;

public class TaskCreateUpdateCommand
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// State, ETaskState
    /// </summary>
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