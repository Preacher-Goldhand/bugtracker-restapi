using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models.AuthenticationDtos
{
    public class RegisterEmployeeDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Department { get; set; }

        [Required]
        [MaxLength(30)]
        public string EmployeeEmail { get; set; }

        [Required]
        public string EmployeePasswordHash { get; set; }

        [Required]
        public string ConfirmEmployeePasswordHash { get; set; }

        public int RoleId { get; set; } = 1;

        public string EmployeePhoneNumber { get; set; }
        public string EmployeeStatus { get; set; }
    }
}