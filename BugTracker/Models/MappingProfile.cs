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
        }
    }
}