using FluentValidation;
using TaskList.Dto.Task.Commands;

namespace TaskList.Dto.Task.Validation
{
    public class TaskSearchFilterValidator<T> : AbstractValidator<T> where T : TaskSearchFilter
    {
        public TaskSearchFilterValidator()
        {
            ValidateSkipValue();
            ValidateLimitValue();
            ValidateSortOrderDirectionValue();
        }

        protected void ValidateSkipValue()
        {
            RuleFor(c => c.Skip)
                .GreaterThanOrEqualTo(0)
                .When(c => c.Skip.HasValue)
                .WithMessage("Please, enter positive number of elements to be skipped");
        }
        
        protected void ValidateLimitValue()
        {
            RuleFor(c => c.Limit)
                .GreaterThanOrEqualTo(0)
                .When(c => c.Limit.HasValue)
                .WithMessage("Please, enter positive limit bound");
        }
        
        protected void ValidateSortOrderDirectionValue()
        {
            RuleFor(c => c.OrderType)
                .IsInEnum()
                .WithMessage("Please, enter order direction value in correct range");
        }
    }
}
