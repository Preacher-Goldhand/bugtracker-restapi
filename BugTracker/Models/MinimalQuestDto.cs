namespace BugTracker.Models
{
    public class MinimalQuestDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public int Priority { get; set; }

        public string TaskStatus { get; set; }

        public int AssignerId { get; set; }

        public virtual EmployeeDto Assigner { get; set; }

        public int AssigneeId { get; set; }

        public virtual EmployeeDto Assignee { get; set; }
    }
}