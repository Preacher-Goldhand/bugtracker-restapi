using BugTracker.Entities;
using BugTracker.Models.Pagination;
using FluentValidation;

namespace BugTracker.Models.Validators
{
    public class QuestPaginationQueryValidator : AbstractValidator<PaginationQuery>
    {
        private int[] allowedPagedSizes = new[] { 5, 10, 15 };
        private string[] allowedSortByColumnNames = { nameof(Quest.Name), nameof(Quest.Category), nameof(Quest.Description) };

        public QuestPaginationQueryValidator()
        {
            RuleFor(q => q.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(q => q.PageSize).Custom((value, context) =>
            {
                if (!allowedPagedSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", allowedPagedSizes)}]");
                }
            });

            RuleFor(e => e.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}