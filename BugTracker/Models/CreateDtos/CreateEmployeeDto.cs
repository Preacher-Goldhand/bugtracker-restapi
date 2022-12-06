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
        [MaxLength(50)]
        public string Department { get; set; }

        [Required]
        [MaxLength(50)]
        public string EmployeeEmail { get; set; }

        public string EmployeePhoneNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string EmployeeStatus { get; set; }
    }
}