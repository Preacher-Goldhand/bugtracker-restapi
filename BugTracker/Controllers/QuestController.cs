﻿using BugTracker.Models;
using BugTracker.Models.CreateDtos;
using BugTracker.Models.Pagination;
using BugTracker.Models.UpdateDtos;
using BugTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    [ApiController]
    [Authorize]
    [Route("bugtracker/board/{boardId}/task")]
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

            return Ok(id);
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<QuestDto>> GetAllQuests(int boardId, [FromQuery] PaginationQuery questQuery)
        //{
        //    var questDtos = _questService.GetAll(boardId, questQuery);
        //    return Ok(questDtos);
        //}

        [HttpPost("{taskId}/comment")]
        public ActionResult AddComment([FromRoute] int taskId, [FromBody] CreateTaskCommentDto dto)
        {
            var taskComment = _questService.AddComment(taskId, dto);
            return Ok(taskComment?.Id);
        }

        [HttpGet("{taskId}/comments")]
        public ActionResult<IEnumerable<TaskCommentDto>> GetAllComments([FromRoute] int taskId)
        {
            var taskComments = _questService.GetAllComments(taskId);
            return Ok(taskComments);
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