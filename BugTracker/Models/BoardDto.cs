namespace BugTracker.Models
{
    public class BoardDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }

        public List<MinimalQuestDto> BoardTasks { get; set; }
    }
}