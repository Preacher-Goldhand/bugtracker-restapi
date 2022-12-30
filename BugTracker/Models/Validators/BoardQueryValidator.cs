using BugTracker.Models.QueryModels;
using FluentValidation;

namespace BugTracker.Models.Validators
{
    public class BoardQueryValidator : AbstractValidator<BoardQuery>
    {
        private int[] allowedPagedSizes = new[] { 5, 10, 15, 30 };

        public BoardQueryValidator()
        {
            RuleFor(b => b.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(b => b.PageSize).Custom((value, context) =>
            {
                if (!allowedPagedSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", allowedPagedSizes)}]");
                }
            });
        }
    }
}