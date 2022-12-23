namespace BugTracker.Models.AuthenticationDtos
{
    public class LoginEmployeeDto
    {
        public string EmployeeEmail { get; set; }
        public string EmployeePasswordHash { get; set; }
    }
}