using Microsoft.EntityFrameworkCore;

namespace BugTracker.Entities
{
    public class BugTrackerDbContext : DbContext
    {
        public BugTrackerDbContext(DbContextOptions<BugTrackerDbContext> options) : base(options)
        { }

        public DbSet<Board> Boards { get; set; }
        public DbSet<Quest> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Quest>()
                .HasMany(e => e.TaskComments)
                .WithOne(e => e.Quest)
                .HasForeignKey(e => e.TaskId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder
                .Entity<Quest>()
                .HasOne(p => p.Assigner)
                .WithMany()
                .HasForeignKey("AssignerId")
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                 .AddJsonFile("appsettings.json", optional: false)
                 .AddJsonFile($"appsettings.{envName}.json", optional: false)
                 .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BugTrackerDb"));
        }
    }
}