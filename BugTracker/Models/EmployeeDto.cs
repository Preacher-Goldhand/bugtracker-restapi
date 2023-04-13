using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class EmployeeDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Department { get; set; }

        [Required]
        [MaxLength(30)]
        public string EmployeeEmail { get; set; }

        public string EmployeePhoneNumber { get; set; }

        public string EmployeeStatus { get; set; }

        // Future features of the application
        public List<QuestDto> AssignerTasks { get; set; } = new List<QuestDto>();

        public List<QuestDto> AssigneeTasks { get; set; } = new List<QuestDto>();
    }
}