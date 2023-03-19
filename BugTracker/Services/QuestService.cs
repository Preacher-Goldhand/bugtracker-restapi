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
    public interface IQuestService
    {
        int Create(int boardId, CreateQuestDto dto);

        PagedResult<QuestDto> GetAll(int boardId, PaginationQuery questQuery);

        QuestDto GetById(int boardId, int questId);

        void Update(int boardId, int questId, UpdateQuestDto dto);

        void Delete(int boardId, int questId);

        //void Assign(int boardId, int questId, AssignQuestDto dto);
    }

    public class QuestService : IQuestService
    {
        private readonly BugTrackerDbContext _dbContext;
        private readonly IMapper _mapper;

        public QuestService(BugTrackerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(int boardId, CreateQuestDto dto)
        {
            if (!_dbContext
                .Tasks
                .Any(t => t.Name == dto.Name))
            {
                throw new AlreadyExistsException("Task already exists");
            }

            var quest = _mapper.Map<Quest>(dto);
            quest.BoardId = boardId;

            _dbContext.Tasks.Add(quest);
            _dbContext.SaveChanges();
            return quest.Id;
        }

        public PagedResult<QuestDto> GetAll(int boardId, PaginationQuery questQuery)
        {
            var baseQuery = _dbContext
              .Boards
              .Include(t => t.BoardTasks)
              .FirstOrDefault(b => b.Id == boardId);

            var boardTasks = baseQuery.BoardTasks
              .Where(b => questQuery.SearchPhrase == null || (b.Name.ToLower().Contains(questQuery.SearchPhrase.ToLower()))
              || b.Category.ToLower().Contains(questQuery.SearchPhrase.ToLower()) || (b.Description.ToLower().Contains(questQuery.SearchPhrase.ToLower())));

            var quests = boardTasks
               .Skip(questQuery.PageSize * (questQuery.PageNumber - 1))
               .Take(questQuery.PageSize)
               .ToList();

            var totalItemsCount = boardTasks.Count();

            var questsDtos = _mapper.Map<List<QuestDto>>(quests);

            var pagedResult = new PagedResult<QuestDto>(questsDtos, totalItemsCount, questQuery.PageSize, questQuery.PageNumber);
            return pagedResult;
        }

        public QuestDto GetById(int boardId, int questId)
        {
            var board = GetBoardsById(boardId);

            var quest = GetQuestsById(boardId, questId);

            var questDto = _mapper.Map<QuestDto>(quest);
            return questDto;
        }

        public void Update(int boardId, int questId, UpdateQuestDto dto)
        {
            var board = GetBoardsById(boardId);

            var quest = GetQuestsById(boardId, questId);

            quest.Name = dto.Name;
            quest.Description = dto.Description;
            quest.Category = dto.Category;
            quest.PropsalDate = dto.PropsalDate;
            quest.LoggedTime = dto.LoggedTime;
            quest.Priority = dto.Priority;
            quest.TaskStatus = dto.TaskStatus;

            // Przypisanie pracownika, który przypisał zadanie
            var assigner = _dbContext.Employees.FirstOrDefault(e => e.FirstName == dto.AssignerFirstName && e.LastName == dto.AssignerLastName);
            if (assigner == null)
            {
                throw new NotFoundException("Assigner not found");
            }
            quest.AssignerId = assigner.Id;

            // Przypisanie pracownika, który ma zadanie do wykonania
            var assignee = _dbContext.Employees.FirstOrDefault(e => e.FirstName == dto.AssigneeFirstName && e.LastName == dto.AssigneeLastName);
            if (assignee == null)
            {
                throw new NotFoundException("Assignee not found");
            }
            quest.AssigneeId = assignee.Id;

            _dbContext.SaveChanges();
        }

        public void Delete(int boardId, int questId)
        {
            var board = GetBoardsById(boardId);

            var quest = GetQuestsById(boardId, questId);

            _dbContext.Tasks.Remove(quest);
            _dbContext.SaveChanges();
        }

        private Board GetBoardsById(int boardId)
        {
            var board = _dbContext
               .Boards
               .FirstOrDefault(b => b.Id == boardId);

            if (board == null)
                throw new NotFoundException("Board not found");
            return board;
        }

        private Quest GetQuestsById(int boardId, int questId)
        {
            var quest = _dbContext
               .Tasks
               .FirstOrDefault(t => t.Id == questId);

            if (quest == null || quest.BoardId != boardId)
                throw new NotFoundException("Task not found");
            return quest;
        }
    }
}