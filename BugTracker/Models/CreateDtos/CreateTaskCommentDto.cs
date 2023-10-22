namespace BugTracker.Models.CreateDtos
{
    public class CreateTaskCommentDto
	{
        public int TaskId { get; set; }

        public string Description { get; set; }

        public DateTime DateOfCreation { get; set; }

        public int UserCreatedId { get; set; }
    }
}

