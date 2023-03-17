using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Models.UpdateDtos
{
    public class UpdateQuestDto
    {
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [MaxLength(20)]
        public string Category { get; set; }

        public DateTime PropsalDate { get; set; }
        public float LoggedTime { get; set; }
        public int Priority { get; set; }

        [MaxLength(20)]
        public string TaskStatus { get; set; }
    }
}