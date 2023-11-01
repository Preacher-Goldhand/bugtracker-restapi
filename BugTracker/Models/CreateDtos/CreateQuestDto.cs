namespace BugTracker.Models.CreateDtos
{
    public class CreateQuestDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DateTime PropsalDate { get; set; }

        public string TaskStatus { get; set; }

        public int AssignerId { get; set; }

        public int AssigneeId { get; set; }

        public float LoggedTime { get; set; }

        public int Priority { get; set; }
        public int StoryPoints { get; set; }

        public int BoardId { get; set; }
    }
}