using BugTracker.Models;
using BugTracker.Models.CreateDtos;
using BugTracker.Models.Pagination;
using BugTracker.Models.UpdateDtos;
using BugTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    [ApiController]
    [Route("bugtracker/employee")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeDto>> GetAllEmployees([FromQuery] PaginationQuery employeeQuery)
        {
            var employeeDtos = _employeeService.GetAll(employeeQuery);
            return Ok(employeeDtos);
        }

        [HttpGet("short")]
        public ActionResult<IEnumerable<EmployeeShortDto>> GetAllEmployeesShort()
        {
            var employeeShortDtos = _employeeService.GetShortAll();
            return Ok(employeeShortDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<EmployeeDto> GetEmployeeById([FromRoute] int id)
        {
            EmployeeDto employeeDto = _employeeService.GetById(id);
            return Ok(employeeDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult UpdateEmployee([FromBody] UpdateEmployeeDto dto, [FromRoute] int id)
        {
            _employeeService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult DeleteEmployee([FromRoute] int id)
        {
            _employeeService.Delete(id);
            return Ok();
        }
    }
}