using BugTracker.Entities;

namespace BugTracker.Models
{
    public class MinimalQuestDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public int StoryPoints { get; set; }

        public float LoggedTime { get; set; }

        public DateTime ProposalDate { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public string TaskStatus { get; set; }

        public int AssignerId { get; set; }

        public virtual EmployeeDto Assigner { get; set; }

        public string AssigneeName { get; set; }

        public ICollection<TaskCommentDto> TaskComments { get; set; } = new List<TaskCommentDto>();
    }
}