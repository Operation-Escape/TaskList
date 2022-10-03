using FluentValidation;
using TaskList.Dto.Task.Commands;

namespace TaskList.Dto.Task.Validation
{
    public class TaskCreateUpdateCommandValidator<T> : AbstractValidator<T> where T : TaskCreateUpdateCommand
    {
        public TaskCreateUpdateCommandValidator()
        {
            ValidateNameValue();
            ValidateStateValue();
            ValidateOrginalEstimateValue();
            ValidateRemainingWorkValue();
            ValidateCompletedWorkValue();
        }

        protected void ValidateNameValue()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(1, 255).WithMessage("The Name must have between 1 and 255 characters");
        }

        protected void ValidateStateValue()
        {
            RuleFor(c => c.State)
                .IsInEnum()
                .WithMessage("Please, enter order task state value in correct range");
        }

        protected void ValidateOrginalEstimateValue()
        {
            RuleFor(c => c.OrginalEstimate)
                .GreaterThanOrEqualTo(0)
                .When(c => c.OrginalEstimate.HasValue)
                .WithMessage("Please, enter positive orginal estimate");
        }

        protected void ValidateRemainingWorkValue()
        {
            RuleFor(c => c.RemainingWork)
                .GreaterThanOrEqualTo(0)
                .When(c => c.RemainingWork.HasValue)
                .WithMessage("Please, enter positive remaining time for work");
        }
        
        protected void ValidateCompletedWorkValue()
        {
            RuleFor(c => c.CompletedWork)
                .GreaterThanOrEqualTo(0)
                .When(c => c.CompletedWork.HasValue)
                .WithMessage("Please, enter positive completed time for work");
        }
    }
}
