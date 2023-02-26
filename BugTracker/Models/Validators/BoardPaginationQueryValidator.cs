using BugTracker.Entities;
using BugTracker.Models.Pagination;
using FluentValidation;

namespace BugTracker.Models.Validators
{
    public class BoardPaginationQueryValidator : AbstractValidator<PaginationQuery>
    {
        private int[] allowedPagedSizes = new[] { 5, 10, 15 };
        private string[] allowedSortByColumnNames = { nameof(Board.Name), nameof(Board.DateOfCreation) };

        public BoardPaginationQueryValidator()
        {
            RuleFor(b => b.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(b => b.PageSize).Custom((value, context) =>
            {
                if (!allowedPagedSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", allowedPagedSizes)}]");
                }
            });

            RuleFor(b => b.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}