using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BugTracker.Entities
{
    public class BugTrackerDbContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Quest> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Required elements of Boards table
            modelBuilder.Entity<Board>()
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Required elements of Tasks table
            modelBuilder.Entity<Quest>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Quest>()
                .Property(t => t.TaskStatus)
                .IsRequired();

            //modelBuilder.Entity<Quest>()
            //    .HasOne(t => t.Assigner)
            //    .WithMany(t => t.AssignerTasks)
            //    .HasForeignKey(t => t.AssignerId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            //modelBuilder.Entity<Quest>()
            //    .HasOne(t => t.Assignee)
            //    .WithMany(t => t.AssigneeTasks)
            //    .HasForeignKey(t => t.AssigneeId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            // Required elements of Employees table
            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            // Required elements of Roles table
            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);
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