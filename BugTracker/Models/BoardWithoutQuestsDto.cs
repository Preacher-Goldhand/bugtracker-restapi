using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class BoardWithoutQuestsDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }
    }
}