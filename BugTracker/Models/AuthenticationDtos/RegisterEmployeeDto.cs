using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models.AuthenticationDtos
{
    public class RegisterEmployeeDto
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

        [Required]
        [MaxLength(50)]
        public string EmployeePasswordHash { get; set; }

        public int RoleId { get; set; } = 1;

        public string EmployeePhoneNumber { get; set; }
        public string EmployeeStatus { get; set; }
    }
}