using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Models.CreateDtos
{
    public class CreateEmployeeDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Department { get; set; }

        [Required]
        [MaxLength(30)]
        public string EmployeeEmail { get; set; }

        public string EmployeePasswordHash { get; set; }
        public string ConfirmPasswordHash { get; set; }

        public int RoleId { get; set; } = 1;

        public string EmployeePhoneNumber { get; set; }

        public string EmployeeStatus { get; set; }
    }
}