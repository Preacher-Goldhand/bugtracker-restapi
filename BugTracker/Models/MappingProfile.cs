using AutoMapper;
using BugTracker.Entities;
using BugTracker.Models.CreateDtos;

namespace BugTracker.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Board, BoardDto>();

            CreateMap<Quest, QuestDto>()
                .ForMember(m => m.AssignerFirstName, c => c.MapFrom(s => s.Assigner.FirstName))
                .ForMember(m => m.AssignerLastName, c => c.MapFrom(s => s.Assigner.LastName))
                .ForMember(m => m.AssignerDepartment, c => c.MapFrom(s => s.Assigner.Department))
                .ForMember(m => m.AssigneeFirstName, c => c.MapFrom(s => s.Assignee.FirstName))
                .ForMember(m => m.AssigneeLastName, c => c.MapFrom(s => s.Assignee.LastName))
                .ForMember(m => m.AssigneeDepartment, c => c.MapFrom(s => s.Assignee.Department));

            CreateMap<CreateBoardDto, Board>();

            //  CreateMap<CreateEmployeeDto, Employee>();

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
        }
    }
}