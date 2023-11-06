namespace BugTracker.Models
{
    public class MyTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Category { get; set; }

        public int Priority { get; set; }

        public string Status { get; set; }

        public int AssignerId { get; set; }

        public virtual EmployeeShortDto Assigner { get; set; }

        public int AssigneeId { get; set; }

        public virtual EmployeeShortDto Assignee { get; set; }
    }
}
