using BugTracker.Models.AuthenticationDtos;
using BugTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    [Route("bugtracker/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAcountService _acountService;

        public AccountController(IAcountService acountService)
        {
            _acountService = acountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterEmployee([FromBody] RegisterEmployeeDto dto)
        {
            _acountService.RegisterEmployee(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult LoginEmployee([FromBody] LoginEmployeeDto dto)
        {
            string token = _acountService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}