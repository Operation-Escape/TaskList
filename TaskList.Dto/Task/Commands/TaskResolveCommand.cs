using TaskList.Dto.Task.Commands.Abstract;
using TaskList.Dto.Task.Validation;

namespace TaskList.Dto.Task.Commands;

public class TaskResolveCommand : Command
{
    /// <summary>
    /// Completed time for work
    /// </summary>
    public decimal CompletedWork { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new TaskResolveCommandValidator<TaskResolveCommand>().Validate(this);
        return ValidationResult.IsValid;
    }
}