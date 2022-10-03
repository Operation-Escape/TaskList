using FluentValidation.Results;

namespace TaskList.Dto.Task.Commands
{
    public abstract class Command
    {
        public ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();
    }
}
