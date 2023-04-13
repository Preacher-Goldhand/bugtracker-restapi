namespace BugTracker.Models.AuthenticationDtos
{
    public class ChangePasswordDto
    {
        public string EmployeeEmail { get; set; }
        public string CurrentPasswordHash { get; set; }
        public string NewPasswordHash { get; set; }
    }
}