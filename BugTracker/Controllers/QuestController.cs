using BugTracker.Models;
using BugTracker.Models.CreateDtos;
using BugTracker.Models.UpdateDtos;
using BugTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Controllers
{
    [ApiController]
    [Route("bugtracker/board/{boardId}/task")]
    [Authorize]
    public class QuestController : ControllerBase
    {
        private readonly IQuestService _questService;

        public QuestController(IQuestService questService)
        {
            _questService = questService;
        }

        [HttpPost]
        public ActionResult CreateQuest([FromRoute] int boardId, [FromBody] CreateQuestDto dto)
        {
            var id = _questService.Create(boardId, dto);

            return Created($"/bugtracker/board/{boardId}/task/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<QuestDto>> GetAllQuests([FromRoute] int boardId)
        {
            var questDtos = _questService.GetAll(boardId);
            return Ok(questDtos);
        }

        [HttpGet("{questId}")]
        public ActionResult<QuestDto> GetQuestById([FromRoute] int boardId, [FromRoute] int questId)
        {
            QuestDto questDto = _questService.GetById(boardId, questId);
            return Ok(questDto);
        }

        [HttpPut("{questId}")]
        public ActionResult UpdateQuest([FromBody] UpdateQuestDto dto, [FromRoute] int boardId, [FromRoute] int questId)
        {
            _questService.Update(boardId, questId, dto);
            return Ok();
        }

        [HttpDelete("{questId}")]
        public ActionResult DeleteQuest([FromRoute] int boardId, [FromRoute] int questId)
        {
            _questService.Delete(boardId, questId);
            return Ok();
        }
    }
}