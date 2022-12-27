using BugTracker.Models;
using BugTracker.Models.CreateDtos;
using BugTracker.Models.UpdateDtos;
using BugTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    [ApiController]
    [Route("bugtracker/board")]
    [Authorize]
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
        public ActionResult<IEnumerable<BoardDto>> GetAllBoards()
        {
            var boardDtos = _boardService.GetAll();
            return Ok(boardDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<BoardDto> GetBoardById([FromRoute] int id)
        {
            BoardDto boardDto = _boardService.GetById(id);
            return Ok(boardDto);
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