using AutoMapper;
using BugTracker.Entities;
using BugTracker.Middleware.CustomErrors;
using BugTracker.Models;
using BugTracker.Models.CreateDtos;
using BugTracker.Models.Pagination;
using BugTracker.Models.UpdateDtos;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BugTracker.Services
{
    public interface IQuestService
    {
        int Create(int boardId, CreateQuestDto dto);

        TaskComment? AddComment(int taskId, CreateTaskCommentDto dto);

        void UpdateComment(CreateTaskCommentDto dto);

        IEnumerable<TaskCommentDto> GetAllComments(int taskId);

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
            var quest = _mapper.Map<Quest>(dto);
            quest.BoardId = boardId;

            var board = _dbContext.Boards
                .Include(b => b.BoardTasks)
                .FirstOrDefault(b => b.Id == boardId);

            if (board == null)
            {
                throw new NotFoundException($"Board with id {boardId} not found.");
            }

            board.BoardTasks.Add(quest);

            _dbContext.SaveChanges();
            return quest.Id;
        }

        public TaskComment? AddComment(int taskId, CreateTaskCommentDto dto)
        {
            var taskComment = _mapper.Map<TaskComment>(dto);
            taskComment.TaskId = taskId;

            _dbContext.TaskComments.Add(taskComment);
            _dbContext.SaveChanges();

            return _dbContext.TaskComments.Find(taskComment.Id);
        }

        public IEnumerable<TaskCommentDto> GetAllComments(int taskId)
        {
            var comments = _dbContext.TaskComments
                .Include(p => p.UserCreated)
                .Where(p => p.TaskId == taskId).ToList();

            var commentsDto = _mapper.Map<List<TaskCommentDto>>(comments);
            return commentsDto;
        }

        public void UpdateComment(CreateTaskCommentDto dto)
        {
            var comment = GetCommentById(dto.Id);
            comment.Description = dto.Description;
            comment.FileName = dto.FileName;
            _dbContext.SaveChanges();
        }

        public QuestDto GetById(int boardId, int questId)
        {
            var quest = _dbContext.Tasks
                .Include(t => t.Assignee)
                .Include(t => t.Assigner)
                .Include(t => t.Board)
                .FirstOrDefault(t => t.Id == questId && t.Board.Id == boardId);

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
            quest.ProposalDate = dto.ProposalDate;
            quest.LoggedTime = dto.LoggedTime;
            quest.Priority = dto.Priority;
            quest.TaskStatus = dto.TaskStatus;
            quest.StoryPoints = dto.StoryPoints;

            var assigner = _dbContext.Employees.FirstOrDefault(e => e.FirstName == dto.AssignerFirstName && e.LastName == dto.AssignerLastName);
            if (assigner == null)
            {
                throw new NotFoundException("Assigner not found");
            }
            quest.AssignerId = assigner.Id;

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

        private TaskComment GetCommentById(int commentId)
        {
            var comment = _dbContext
               .TaskComments
               .FirstOrDefault(b => b.Id == commentId);

            if (comment == null)
                throw new NotFoundException("Comment not found");
            return comment;
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