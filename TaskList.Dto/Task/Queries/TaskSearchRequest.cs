namespace TaskList.Dto.Task.Queries;

public class TaskSearchRequest
{
    /// <summary>
    /// Skip of Task
    /// </summary>
    public int? Skip { get; set; }
    /// <summary>
    /// Limit of Task
    /// </summary>
    public int? Limit { get; set; }
    /// <summary>
    /// Sort direction, EOrderType
    /// </summary>
    public int OrderType { get; set; }
}