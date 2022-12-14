using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class QuestDto
    {
        public int Id { get; set; }

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

        [Required]
        [MaxLength(20)]
        public string TaskStatus { get; set; }

        [MaxLength(50)]
        public string AssignerFirstName { get; set; }

        [MaxLength(50)]
        public string AssignerLastName { get; set; }

        [MaxLength(30)]
        public string AssignerDepartment { get; set; }

        [MaxLength(50)]
        public string AssigneeFirstName { get; set; }

        [MaxLength(50)]
        public string AssigneeLastName { get; set; }

        [MaxLength(30)]
        public string AssigneeDepartment { get; set; }
    }
}