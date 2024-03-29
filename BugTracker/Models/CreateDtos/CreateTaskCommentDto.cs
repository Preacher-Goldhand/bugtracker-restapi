﻿namespace BugTracker.Models.CreateDtos
{
    public class CreateTaskCommentDto
	{
        public int Id { get; set; }

        public int TaskId { get; set; }

        public string Description { get; set; }

        public DateTime DateOfCreation { get; set; }

        public int UserCreatedId { get; set; }

        public string? FileName { get; set; }
    }
}

