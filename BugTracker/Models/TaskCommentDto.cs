using BugTracker.Entities;

namespace BugTracker.Models
{
    public class TaskCommentDto
	{
        public int Id { get; set; }

        public int TaskId { get; set; }

        public string Description { get; set; }

        public DateTime DateOfCreation { get; set; }

        public int UserCreatedId { get; set; }

        public virtual Employee UserCreated { get; set; }

        public string? FileName { get; set; }
    }
}

