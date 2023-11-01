using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class QuestDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string Category { get; set; }

        public DateTime DateOfCreation { get; set; }
        public DateTime PropsalDate { get; set; }
        public float LoggedTime { get; set; }
        public int Priority { get; set; }
        public int StoryPoints { get; set; }

        [Required]
        [MaxLength(20)]
        public string TaskStatus { get; set; }

        public string AssignerFirstName { get; set; }
        public string AssignerLastName { get; set; }
        public string AssigneeFirstName { get; set; }
        public string AssigneeLastName { get; set; }
    }
}