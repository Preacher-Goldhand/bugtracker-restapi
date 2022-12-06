using AutoMapper;
using BugTracker.Entities;
using BugTracker.Middleware.CustomErrors;
using BugTracker.Models;
using BugTracker.Models.CreateDtos;
using BugTracker.Models.UpdateDtos;

namespace BugTracker.Services
{
    public interface IEmployeeService
    {
        int Create(CreateEmployeeDto dto);

        IEnumerable<EmployeeDto> GetAll();

        EmployeeDto GetById(int id);

        void Update(int id, UpdateEmployeeDto dto);

        public void Delete(int id);
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

        public int Create(CreateEmployeeDto dto)
        {
            if (_dbContext
                .Employees
                .Any(e => e.FirstName == dto.FirstName))
            {
                throw new AlreadyExistsException("Employee already exists");
            }

            var employee = _mapper.Map<Employee>(dto);
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            return employee.Id;
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var employees = _dbContext
               .Employees
               .ToList();

            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);
            return employeeDtos;
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
    }
}