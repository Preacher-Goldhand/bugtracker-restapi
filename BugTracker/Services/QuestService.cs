using AutoMapper;
using BugTracker.Entities;
using BugTracker.Middleware.CustomErrors;
using BugTracker.Models;
using BugTracker.Models.CreateDtos;
using BugTracker.Models.UpdateDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Services
{
    public interface IQuestService
    {
        int Create(int boardId, CreateQuestDto dto);

        IEnumerable<QuestDto> GetAll(int boardId);

        QuestDto GetById(int boardId, int questId);

        void Update(int boardId, int questId, UpdateQuestDto dto);

        void Delete(int boardId, int questId);
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
            if (_dbContext
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

        public IEnumerable<QuestDto> GetAll(int boardId)
        {
            var quests = _dbContext
              .Boards
              .Include(t => t.BoardTasks)
              .FirstOrDefault(b => b.Id == boardId);

            var questDtos = _mapper.Map<List<QuestDto>>(quests.BoardTasks);
            return questDtos;
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
            quest.Assigner.FirstName = dto.AssignerFirstName;
            quest.Assigner.LastName = dto.AssignerLastName;
            quest.Assigner.Department = dto.AssignerDepartment;
            quest.Assignee.FirstName = dto.AssigneeFirstName;
            quest.Assignee.LastName = dto.AssigneeLastName;
            quest.Assignee.Department = dto.AssigneeDepartment;

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