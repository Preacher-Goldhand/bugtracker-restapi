using BugTracker.Models.AuthenticationDtos;
using BugTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    [Route("bugtracker/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService acountService)
        {
            _accountService = acountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterEmployee([FromBody] RegisterEmployeeDto dto)
        {
            _accountService.RegisterEmployee(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult LoginEmployee([FromBody] LoginEmployeeDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }

        [HttpPost("changePassword")]
        public ActionResult ChangePassword([FromBody] ChangePasswordDto dto)
        {
            _accountService.ChangePassword(dto);
            return Ok();
        }
    }
}