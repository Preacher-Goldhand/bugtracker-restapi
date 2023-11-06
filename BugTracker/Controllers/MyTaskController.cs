using BugTracker.Models.Pagination;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BugTracker.Services;

namespace BugTracker.Controllers
{
    [ApiController]
    [Authorize]
    [Route("bugtracker/mytasks")]
    public class MyTaskController : ControllerBase
    {
        private readonly IMyTaskService _myTaskService;

        public MyTaskController(IMyTaskService myTaskService)
        {
            _myTaskService = myTaskService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MyTaskDto>> GetAssignedTasks([FromQuery] PaginationQuery questQuery)
        {
            ClaimsPrincipal user = HttpContext.User;
            var tasks = _myTaskService.GetAssignedTasks(questQuery, user);
            return Ok(tasks);
        }
    }
}
