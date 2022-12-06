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
        [MaxLength(50)]
        public string Category { get; set; }

        public DateTime DateOfCreation { get; set; }
        public DateTime PropsalDate { get; set; }
        public float LoggedTime { get; set; }
        public int Priority { get; set; }

        [Required]
        [MaxLength(50)]
        public string TaskStatus { get; set; }

        [Required]
        [MaxLength(50)]
        public string AssignerFirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string AssignerLastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string AssignerDepartment { get; set; }

        [Required]
        [MaxLength(50)]
        public string AssigneeFirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string AssigneeLastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string AssigneeDepartment { get; set; }
    }
}