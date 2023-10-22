using System;
using BugTracker.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Models
{
	public class TaskCommentDto
	{
        public int Id { get; set; }

        public int TaskId { get; set; }

        public string Description { get; set; }

        public DateTime DateOfCreation { get; set; }

        public int UserCreatedId { get; set; }

        public virtual Employee UserCreated { get; set; }
    }
}

