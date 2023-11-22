namespace BugTracker.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Department { get; set; }

        public string EmployeeEmail { get; set; }

        public string EmployeePhoneNumber { get; set; }
        public int AvailableHours { get; set; }

        public string EmployeeStatus { get; set; }

        public int RoleId { get; set; }

        // Future features of the application
        public List<QuestDto> AssignerTasks { get; set; } = new List<QuestDto>();

        public List<QuestDto> AssigneeTasks { get; set; } = new List<QuestDto>();
    }
}