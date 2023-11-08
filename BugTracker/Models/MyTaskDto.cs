namespace BugTracker.Models
{
    public class MyTaskDto
    {
        public int Id { get; set; }

        public int BoardId { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public int Priority { get; set; }

        public string Status { get; set; }

        public int StoryPoints { get; set; }

        public float LoggedTime { get; set; }

        public DateTime ProposalDate { get; set; }

        public string Description { get; set; }

        public int AssignerId { get; set; }

        public virtual EmployeeShortDto Assigner { get; set; }

        public int AssigneeId { get; set; }

        public virtual EmployeeShortDto Assignee { get; set; }
    }
}
