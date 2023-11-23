using AutoMapper;
using BugTracker.Entities;
using BugTracker.Models.CreateDtos;

namespace BugTracker.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Board, BoardWithoutQuestsDto>();
            CreateMap<Board, BoardDto>();

            CreateMap<Quest, QuestDto>();

            CreateMap<CreateBoardDto, Board>();

            CreateMap<CreateQuestDto, Quest>();

            CreateMap<Employee, EmployeeDto>()
                .ForMember(m => m.FirstName, c => c.MapFrom(s => s.FirstName))
                .ForMember(m => m.LastName, c => c.MapFrom(s => s.LastName))
                .ForMember(m => m.Department, c => c.MapFrom(s => s.Department))
                .ForMember(m => m.EmployeeEmail, c => c.MapFrom(s => s.EmployeeEmail))
                .ForMember(m => m.EmployeePhoneNumber, c => c.MapFrom(s => s.EmployeePhoneNumber))
                .ForMember(m => m.EmployeeStatus, c => c.MapFrom(s => s.EmployeeStatus))
                .ForMember(m => m.AssignerTasks, c => c.MapFrom(s => s.AssignerTasks))
                .ForMember(m => m.AssigneeTasks, c => c.MapFrom(s => s.AssigneeTasks));

            CreateMap<Quest, QuestDto>()
                .ForMember(dest => dest.AssigneeId, opt => opt.MapFrom(src => src.Assignee.Id))
                .ForMember(dest => dest.AssigneeFirstName, opt => opt.MapFrom(src => src.Assignee.FirstName))
                .ForMember(dest => dest.AssigneeLastName, opt => opt.MapFrom(src => src.Assignee.LastName))
                .ForMember(dest => dest.AssignerId, opt => opt.MapFrom(src => src.Assigner.Id))
                .ForMember(dest => dest.AssignerFirstName, opt => opt.MapFrom(src => src.Assigner.FirstName))
                .ForMember(dest => dest.AssignerLastName, opt => opt.MapFrom(src => src.Assigner.LastName))
                .ForMember(dest => dest.StoryPoints, opt => opt.MapFrom(src => src.StoryPoints));

            CreateMap<Quest, MinimalQuestDto>();
            CreateMap<Quest, MyTaskDto>();

            CreateMap<Employee, EmployeeShortDto>()
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.FirstName, c => c.MapFrom(s => s.FirstName))
                .ForMember(m => m.LastName, c => c.MapFrom(s => s.LastName));

            CreateMap<CreateTaskCommentDto, TaskComment>()
                .ForMember(m => m.TaskId, c => c.MapFrom(s => s.TaskId))
                .ForMember(m => m.Description, c => c.MapFrom(s => s.Description))
                .ForMember(m => m.DateOfCreation, c => c.MapFrom(s => s.DateOfCreation))
                .ForMember(m => m.UserCreatedId, c => c.MapFrom(s => s.UserCreatedId));

            CreateMap<TaskComment, TaskCommentDto>()
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.Description, c => c.MapFrom(s => s.Description))
                .ForMember(m => m.DateOfCreation, c => c.MapFrom(s => s.DateOfCreation))
                .ForMember(m => m.UserCreatedId, c => c.MapFrom(s => s.UserCreatedId))
                .ForMember(m => m.UserCreated, c => c.MapFrom(s => s.UserCreated))
                .ForMember(m => m.FileName, c => c.MapFrom(s => s.FileName));

            CreateMap<TaskCommentDto, TaskComment>()
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.Description, c => c.MapFrom(s => s.Description))
                .ForMember(m => m.DateOfCreation, c => c.MapFrom(s => s.DateOfCreation))
                .ForMember(m => m.UserCreatedId, c => c.MapFrom(s => s.UserCreatedId))
                .ForMember(m => m.UserCreated, c => c.MapFrom(s => s.UserCreated))
                .ForMember(m => m.FileName, c => c.MapFrom(s => s.FileName));
        }
    }
}