using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Models.CreateDtos
{
    public class CreateQuestDto
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

        [Required]
        [MaxLength(20)]
        public string TaskStatus { get; set; }

        public int? AssignerId { get; set; }

        public string AssignerFirstName { get; set; }

        public string AssignerLastName { get; set; }

        public string AssignerDepartment { get; set; }

        public int? AssigneeId { get; set; }

        public string AssigneeFirstName { get; set; }

        public string AssigneeLastName { get; set; }

        public string AssigneeDepartment { get; set; }

        public int BoardId { get; set; }
    }
}