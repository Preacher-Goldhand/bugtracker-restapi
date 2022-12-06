using BugTracker.Models;
using BugTracker.Models.CreateDtos;
using BugTracker.Models.UpdateDtos;
using BugTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    [ApiController]
    [Route("bugtracker/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public ActionResult CreateEmployee([FromBody] CreateEmployeeDto dto)
        {
            var id = _employeeService.Create(dto);

            return Created($"/bugtracker/employee/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            var employeeDtos = _employeeService.GetAll();
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<EmployeeDto> GetEmployeeById([FromRoute] int id)
        {
            EmployeeDto employeeDto = _employeeService.GetById(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateEmployee([FromBody] UpdateEmployeeDto dto, [FromRoute] int id)
        {
            _employeeService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee([FromRoute] int id)
        {
            _employeeService.Delete(id);
            return Ok();
        }
    }
}