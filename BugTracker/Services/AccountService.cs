using BugTracker.Entities;
using BugTracker.Middleware.Custom_Exceptions;
using BugTracker.Models.AuthenticationDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BugTracker.Services
{
    public interface IAccountService
    {
        void RegisterEmployee(RegisterEmployeeDto dto);

        string GenerateJwt(LoginEmployeeDto dto);

        //void Logout(string jwt);

        void ChangePassword(ChangePasswordDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly BugTrackerDbContext _dbContext;
        private readonly IPasswordHasher<Employee> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(BugTrackerDbContext dbContext, IPasswordHasher<Employee> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
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

        public string GenerateJwt(LoginEmployeeDto dto)
        {
            var employee = _dbContext.Employees
                .Include(e => e.Role)
                .FirstOrDefault(e => e.EmployeeEmail == dto.EmployeeEmail);

            if (employee == null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var passwordHash = _passwordHasher.VerifyHashedPassword(employee, employee.EmployeePasswordHash, dto.EmployeePasswordHash);
            if (passwordHash == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{employee.FirstName } {employee.LastName}"),
                new Claim(ClaimTypes.Role, $"{employee.Role.Name}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                        _authenticationSettings.JwtIssuer, claims,
                        expires: expires, signingCredentials: creds);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

        //public void Logout(string jwt)
        //{
        //    _activeJwtTokens.Remove(jwt);
        //}

        public void ChangePassword(ChangePasswordDto dto)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmployeeEmail == dto.EmployeeEmail);

            if (employee == null)
            {
                throw new BadRequestException("Employee not found");
            }

            var isCurrentPasswordValid = _passwordHasher.VerifyHashedPassword(employee, employee.EmployeePasswordHash, dto.CurrentPasswordHash);

            if (isCurrentPasswordValid == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid password");
            }

            // Zmień hasło na nowe
            var newHashedPassword = _passwordHasher.HashPassword(employee, dto.NewPasswordHash);
            employee.EmployeePasswordHash = newHashedPassword;

            _dbContext.SaveChanges();
        }
    }
}