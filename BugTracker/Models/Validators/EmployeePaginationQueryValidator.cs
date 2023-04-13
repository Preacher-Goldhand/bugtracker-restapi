using BugTracker.Entities;
using BugTracker.Models.Pagination;
using FluentValidation;

namespace BugTracker.Models.Validators
{
    public class EmployeePaginationQueryValidator : AbstractValidator<PaginationQuery>
    {
        private int[] allowedPagedSizes = new[] { 5, 10, 15 };
        private string[] allowedSortByColumnNames = { nameof(Employee.FirstName), nameof(Employee.LastName), nameof(Employee.Department) };

        public EmployeePaginationQueryValidator()
        {
            RuleFor(e => e.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(e => e.PageSize).Custom((value, context) =>
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