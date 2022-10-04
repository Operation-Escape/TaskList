using TaskList.Dto.Enums;
using TaskList.Dto.Task.Commands.Abstract;
using TaskList.Dto.Task.Validation;

namespace TaskList.Dto.Task.Commands;

public class TaskSearchFilter : Command
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
    public EOrderDirection OrderType { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new TaskSearchFilterValidator<TaskSearchFilter>().Validate(this);
        return true;
    }
}