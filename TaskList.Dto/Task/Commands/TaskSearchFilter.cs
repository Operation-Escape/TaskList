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
    public int OrderType { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new TaskSearchFilterValidator<TaskSearchFilter>().Validate(this);
        if (!ValidationResult.IsValid)
            throw new ArgumentException(ValidationResult.ToString());

        return true;
    }
}