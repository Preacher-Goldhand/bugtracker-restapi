using AutoMapper;
using BugTracker.Entities;
using BugTracker.Middleware.CustomErrors;
using BugTracker.Models;
using BugTracker.Models.CreateDtos;
using BugTracker.Models.Pagination;
using BugTracker.Models.UpdateDtos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BugTracker.Services
{
    public interface IEmployeeService
    {
        PagedResult<EmployeeDto> GetAll(PaginationQuery employeeQuery);

        IEnumerable<EmployeeShortDto> GetShortAll();

        EmployeeDto GetById(int id);

        int GetAvailableHours(int id);

        void Update(int id, UpdateEmployeeDto dto);

        void Delete(int id);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly BugTrackerDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeeService(BugTrackerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public PagedResult<EmployeeDto> GetAll(PaginationQuery employeeQuery)
        {
            var baseQuery = _dbContext
               .Employees
               .Where(e => employeeQuery.SearchPhrase == null || (e.FirstName.ToLower().Contains(employeeQuery.SearchPhrase.ToLower()))
               || (e.LastName.ToLower().Contains(employeeQuery.SearchPhrase.ToLower())) || (e.Department.ToLower().Contains(employeeQuery.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(employeeQuery.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Employee, object>>>
                {
                    { nameof(Employee.FirstName), e => e.FirstName },
                    { nameof(Employee.LastName), e => e.LastName },
                    { nameof(Employee.Department), e => e.Department},
                };

                var selectedColumn = columnsSelectors[employeeQuery.SortBy];

                baseQuery = employeeQuery.SortOrder == SortDirection.ASC
                            ? baseQuery.OrderBy(selectedColumn)
                            : baseQuery.OrderByDescending(selectedColumn);
            }
            var employees = baseQuery
               .Skip(employeeQuery.PageSize * (employeeQuery.PageNumber - 1))
               .Take(employeeQuery.PageSize)
               .ToList();

            var totalItemsCount = baseQuery.Count();

            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            var pagedResult = new PagedResult<EmployeeDto>(employeeDtos, totalItemsCount, employeeQuery.PageSize, employeeQuery.PageNumber);
            return pagedResult;
        }

        public IEnumerable<EmployeeShortDto> GetShortAll()
        {
            var employees = _dbContext.Employees.Where(p => p.EmployeeStatus.Equals("Active")).ToList();
            return _mapper.Map<IEnumerable<EmployeeShortDto>>(employees);
        }

        public EmployeeDto GetById(int id)
        {
            var employee = _dbContext
               .Employees
               .FirstOrDefault(t => t.Id == id);

            if (employee == null)
                throw new NotFoundException("Employee not found");

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public void Update(int id, UpdateEmployeeDto dto)
        {
            var employee = _dbContext
                .Employees
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                throw new NotFoundException("Employee not found");

            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Department = dto.Department;
            employee.EmployeeEmail = dto.EmployeeEmail;
            employee.EmployeePhoneNumber = dto.EmployeePhoneNumber;
            employee.AvailableHours = dto.AvailableHours;
            employee.RoleId = dto.RoleId;
            employee.EmployeeStatus = dto.EmployeeStatus;

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var employee = _dbContext
              .Employees
              .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                throw new NotFoundException("Employee not found");

            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
        }

        public int GetAvailableHours(int id)
        {
            var statuses = new string[] { "TO_DO", "IN_PROGRESS" };

            var storyPoints =
                _dbContext
                .Tasks
                .Where(
                    p => p.AssigneeId == id &&
                    statuses.Contains(p.TaskStatus)
                 )
                .Select(p => p.StoryPoints)
                .Sum();

            var employee =
                _dbContext
                .Employees
                .FirstOrDefault(p => p.Id == id);

            if (employee == null)
                throw new NotFoundException("Employee not found");

            var result = (employee.AvailableHours - storyPoints);

            return result > 0 ? result : 0;
        }
    }
}