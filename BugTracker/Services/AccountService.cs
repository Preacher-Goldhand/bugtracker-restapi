using BugTracker.Entities;
using BugTracker.Models.AuthenticationDtos;
using Microsoft.AspNetCore.Identity;

namespace BugTracker.Services
{
    public interface IAcountService
    {
        void RegisterEmployee(RegisterEmployeeDto dto);
    }

    public class AccountService : IAcountService
    {
        private readonly BugTrackerDbContext _dbContext;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public AccountService(BugTrackerDbContext dbContext, IPasswordHasher<Employee> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public void RegisterEmployee(RegisterEmployeeDto dto)
        {
            var newEmployee = new Employee()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Department = dto.Department,
                EmployeeEmail = dto.EmployeeEmail,
                RoleId = dto.RoleId,
                EmployeePhoneNumber = dto.EmployeePhoneNumber,
                EmployeeStatus = dto.EmployeeStatus
            };
            var hashedPassword = _passwordHasher.HashPassword(newEmployee, dto.EmployeePasswordHash);
            newEmployee.EmployeePasswordHash = hashedPassword;

            _dbContext.Employees.Add(newEmployee);
            _dbContext.SaveChanges();
        }
    }
}