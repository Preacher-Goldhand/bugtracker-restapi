using BugTracker.Entities;
using BugTracker.Models.AuthenticationDtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models.Validators
{
    public class RegisterEmployeeDtoValidator : AbstractValidator<RegisterEmployeeDto>
    {
        public RegisterEmployeeDtoValidator(BugTrackerDbContext dbContext)
        {
            RuleFor(x => x.FirstName)
              .NotEmpty()
              .MaximumLength(50);

            RuleFor(x => x.LastName)
              .NotEmpty()
              .MaximumLength(50);

            RuleFor(x => x.Department)
             .NotEmpty()
             .MaximumLength(30);

            RuleFor(x => x.EmployeeEmail)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(30);

            RuleFor(x => x.EmployeePasswordHash).MinimumLength(6);

            RuleFor(x => x.ConfirmEmployeePasswordHash).Equal(e => e.EmployeePasswordHash);

            RuleFor(x => x.EmployeeEmail)
               .Custom((value, context) =>
               {
                   var emailInUse = dbContext.Employees.Any(e => e.EmployeeEmail == value);
                   if (emailInUse)
                   {
                       context.AddFailure("EmployeeEmail", "Email is taken");
                   }
               });
        }
    }
}