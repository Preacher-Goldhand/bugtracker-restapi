namespace BugTracker.Entities
{
    public class DataSeeder
    {
        private readonly BugTrackerDbContext _dbContext;

        public DataSeeder(BugTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Boards.Any())
                {
                    var boards = GetBoards();
                    _dbContext.Boards.AddRange(boards);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }

        private IEnumerable<Board> GetBoards()
        {
            var boards = new List<Board>()
            {
                new Board()
                {
                    Name = "MyBoard",
                    DateOfCreation = new DateTime(2022, 1, 1),         //yyy-mm-dd
                    BoardTasks = new List<Quest>()
                    {
                        new Quest()
                        {
                            Name = "MyTask",
                            Description = "MyTask description",
                            Category = "Development Task",
                            DateOfCreation = new DateTime(2022, 1, 1), //yyy-mm-dd
                            PropsalDate = new DateTime(2022, 1, 31),   //yyy-mm-dd
                            LoggedTime = 1.5f,
                            Priority = 3,
                            TaskStatus = "ToDo",
                            Assigner = new Employee()
                            {
                                FirstName = "John",
                                LastName = "Snow",
                                Department = "DevTool Department",
                                EmployeeEmail = "jsnow@test.com",
                                EmployeePasswordHash = "password",
                                RoleId = 2,
                                EmployeePhoneNumber = "753159852",
                                EmployeeStatus = "Active",
                            },
                            Assignee = new Employee()
                            {
                                FirstName = "Emma",
                                LastName = "Watson",
                                Department = "DevTool Department",
                                EmployeeEmail = "ewatson@test.com",
                                EmployeePasswordHash = "password123",
                                RoleId = 1,
                                EmployeePhoneNumber = "700159002",
                                EmployeeStatus = "Active"
                            },
                            Board = new Board()
                            {
                                Name = "MyBoard"
                            }
                        }
                    }
                }
            };
            return boards;
        }
    }
}