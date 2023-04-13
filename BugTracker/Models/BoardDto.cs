using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class BoardDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }

        public List<QuestDto> BoardTasks { get; set; }
    }
}