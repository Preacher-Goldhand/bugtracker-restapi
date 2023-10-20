namespace BugTracker.Models.CreateDtos
{
    public class CreateBoardDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }
    }
}