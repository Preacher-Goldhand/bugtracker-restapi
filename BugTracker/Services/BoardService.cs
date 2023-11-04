using AutoMapper;
using BugTracker.Entities;
using BugTracker.Middleware.CustomErrors;
using BugTracker.Models;
using BugTracker.Models.CreateDtos;
using BugTracker.Models.Pagination;
using BugTracker.Models.UpdateDtos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BugTracker.Services
{
    public interface IBoardService
    {
        int Create(CreateBoardDto dto);

        PagedResult<BoardWithoutQuestsDto> GetAll(PaginationQuery boardQuery);

        BoardDto GetById(int id);

        void Update(int id, UpdateBoardDto dto);

        void Delete(int id);
    }

    public class BoardService : IBoardService
    {
        private readonly BugTrackerDbContext _dbContext;
        private readonly IMapper _mapper;

        public BoardService(BugTrackerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(CreateBoardDto dto)
        {
            if (_dbContext
                .Boards
                .Any(b => b.Name == dto.Name))
            {
                throw new AlreadyExistsException("Board already exists");
            }

            var board = _mapper.Map<Board>(dto);
            _dbContext.Boards.Add(board);
            _dbContext.SaveChanges();
            return board.Id;
        }

        public PagedResult<BoardWithoutQuestsDto> GetAll(PaginationQuery boardQuery)
        {
            var baseQuery = _dbContext
               .Boards
               .Include(b => b.BoardTasks)
               .Where(b => boardQuery.SearchPhrase == null || (b.Name.ToLower().Contains(boardQuery.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(boardQuery.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Board, object>>>
                {
                    { nameof(Board.Name), b => b.Name },
                    { nameof(Board.DateOfCreation), b => b.DateOfCreation },
                };

                var selectedColumn = columnsSelectors[boardQuery.SortBy];

                baseQuery = boardQuery.SortOrder == SortDirection.ASC
                            ? baseQuery.OrderBy(selectedColumn)
                            : baseQuery.OrderByDescending(selectedColumn);
            }
            var boards = baseQuery
               .Skip(boardQuery.PageSize * (boardQuery.PageNumber - 1))
               .Take(boardQuery.PageSize)
               .ToList();

            var totalItemsCount = baseQuery.Count();

            var boardDtos = _mapper.Map<List<BoardWithoutQuestsDto>>(boards);

            var pagedResult = new PagedResult<BoardWithoutQuestsDto>(boardDtos, totalItemsCount, boardQuery.PageSize, boardQuery.PageNumber);
            return pagedResult;
        }

        public BoardDto GetById(int id)
        {
            var board = _dbContext
               .Boards
               .Include(p => p.BoardTasks)
                    .ThenInclude(p => p.Assigner)
                .Include(p => p.BoardTasks)
                    .ThenInclude(p => p.TaskComments)
                    .ThenInclude(p => p.UserCreated)
               .FirstOrDefault(p => p.Id == id);

            if (board == null)
                throw new NotFoundException("Board not found");

            var boardDto = _mapper.Map<BoardDto>(board);
            return boardDto;
        }

        public void Update(int id, UpdateBoardDto dto)
        {
            var board = _dbContext
                .Boards
                .FirstOrDefault(b => b.Id == id);

            if (board == null)
                throw new NotFoundException("Board not found");

            board.Name = dto.Name;

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var board = _dbContext
              .Boards
              .FirstOrDefault(b => b.Id == id);

            if (board == null)
                throw new NotFoundException("Board not found");

            _dbContext.Boards.Remove(board);
            _dbContext.SaveChanges();
        }
    }
}