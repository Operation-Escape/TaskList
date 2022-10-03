using FluentValidation;
using TaskList.Dto.Task.Commands;

namespace TaskList.Dto.Task.Validation
{
    public class TaskResolveCommandValidator<T> : AbstractValidator<T> where T : TaskResolveCommand
    {
        public TaskResolveCommandValidator()
        {
            ValidateCompletedWorkValue();
        }

        protected void ValidateCompletedWorkValue()
        {
            RuleFor(c => c.CompletedWork)
                .GreaterThanOrEqualTo(0.0m).WithMessage("Please, ensure you have entered the correct time");
        }
    }
}
