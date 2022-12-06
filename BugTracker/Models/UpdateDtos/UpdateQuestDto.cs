using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Models.UpdateDtos
{
    public class UpdateQuestDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Category { get; set; }

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