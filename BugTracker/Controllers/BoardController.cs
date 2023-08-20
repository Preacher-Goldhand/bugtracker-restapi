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
    [Authorize]
    [Route("bugtracker/board")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpPost]
        public ActionResult CreateBoard([FromBody] CreateBoardDto dto)
        {
            var id = _boardService.Create(dto);

            return Created($"/bugtracker/board/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<BoardDto>> GetAllBoards([FromQuery] PaginationQuery boardQuery)
        {
            var boardDtos = _boardService.GetAll(boardQuery);
            return Ok(boardDtos);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBoard([FromBody] UpdateBoardDto dto, [FromRoute] int id)
        {
            _boardService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBoard([FromRoute] int id)
        {
            _boardService.Delete(id);
            return Ok();
        }
    }
}