using BugTracker.Models.Pagination;
using BugTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AutoMapper;
using BugTracker.Entities;

namespace BugTracker.Services
{
    public interface IMyTaskService
    {
        PagedResult<MyTaskDto> GetAssignedTasks(PaginationQuery questQuery, ClaimsPrincipal user);
    }

    public class MyTaskService : IMyTaskService
    {
        private readonly BugTrackerDbContext _dbContext;
        private readonly IMapper _mapper;

        public MyTaskService(BugTrackerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public PagedResult<MyTaskDto> GetAssignedTasks(PaginationQuery questQuery, ClaimsPrincipal user)
        {
            var loggedInUserId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);

            var allTasks = _dbContext.Tasks
                .Where(b => b.AssignerId == loggedInUserId)
                .Include(t => t.Assignee)
                .Include(t => t.Assigner)
                .ToList();

            var tasks = allTasks
                .Skip(questQuery.PageSize * (questQuery.PageNumber - 1))
                .Take(questQuery.PageSize)
                .ToList();

            var totalItemsCount = allTasks.Count();

            // Mapowanie danych zadań na DTO z uwzględnieniem informacji o osobach (tylko imię i nazwisko)
            var myTasks = tasks.Select(task => new MyTaskDto
            {
                Id = task.Id,
                BoardId = task.BoardId,
                Name = task.Name,
                Category = task.Category,
                Priority = task.Priority,
                Status = task.TaskStatus,
                StoryPoints = task.StoryPoints,
                LoggedTime = task.LoggedTime,
                ProposalDate = task.ProposalDate,
                Description = task.Description,
                Assigner = new EmployeeShortDto
                {
                    FirstName = task.Assigner.FirstName,
                    LastName = task.Assigner.LastName
                },
                Assignee = new EmployeeShortDto
                {
                    FirstName = task.Assignee.FirstName,
                    LastName = task.Assignee.LastName
                }
            }).ToList();

            var pagedResult = new PagedResult<MyTaskDto>(myTasks, totalItemsCount, questQuery.PageSize, questQuery.PageNumber);

            return pagedResult;
        }
    }
}