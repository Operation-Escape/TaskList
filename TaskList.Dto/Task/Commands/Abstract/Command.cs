using FluentValidation.Results;

namespace TaskList.Dto.Task.Commands.Abstract
{
    public abstract class Command
    {
        public ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();
    }
}
